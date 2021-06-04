using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nova.Editor
{
    [CustomEditor(typeof(UncroppedStanding))]
    public class UncroppedStandingEditor : UnityEditor.Editor
    {
        private static void ResetTransform(Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
        }

        [MenuItem("Assets/Create/Nova/Uncropped Standing", false)]
        public static void CreateUncroppedStandingWithSelectedSprites()
        {
            const string assetName = "UncroppedStanding";
            var parent = new GameObject(assetName);
            ResetTransform(parent.transform);
            parent.AddComponent<UncroppedStanding>();

            foreach (var spritePath in EditorUtils.GetSelectedSpritePaths())
            {
                var go = new GameObject("StandingComponent");
                var spriteRenderer = go.AddComponent<SpriteRenderer>();
                var sprite = spriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                var cropper = go.AddComponent<SpriteCropper>();
                cropper.cropRect = new RectInt(0, 0, sprite.texture.width, sprite.texture.height);
                go.transform.SetParent(parent.transform);
                ResetTransform(go.transform);
            }

            var currentDir = EditorUtils.GetSelectedDirectory();

            PrefabUtility.SaveAsPrefabAsset(parent,
                Path.Combine(currentDir, AssetDatabase.GenerateUniqueAssetPath(assetName + ".prefab")));
            DestroyImmediate(parent);
        }

        [MenuItem("Assets/Create/Nova/Uncropped Standing", true)]
        public static bool CreateUncroppedStandingWithSelectedSpritesValidation()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return AssetDatabase.GetMainAssetTypeAtPath(path) == typeof(Texture2D);
        }

        private bool useCaptureBox;
        private RectInt captureBox = new RectInt(0, 0, 400, 400);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var standing = target as UncroppedStanding;

            useCaptureBox = GUILayout.Toggle(useCaptureBox, "Use Capture Box");
            if (useCaptureBox)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Capture Box");
                captureBox = EditorGUILayout.RectIntField(captureBox);
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Auto Crop All"))
            {
                foreach (var cropper in standing.GetComponentsInChildren<SpriteCropper>())
                {
                    if (useCaptureBox)
                    {
                        SpriteCropperEditor.AutoCrop(cropper, captureBox);
                    }
                    else
                    {
                        Debug.Log($"test1 {cropper.sprite}");
                        SpriteCropperEditor.AutoCrop(cropper);
                    }
                }
            }

            if (GUILayout.Button("Write Cropped Textures"))
            {
                WriteCropResult(standing);
            }

            if (GUILayout.Button("Generate Metadata"))
            {
                GenerateMetaData(standing);
            }
        }

        private static void WriteCropResult(UncroppedStanding standing)
        {
            foreach (var cropper in standing.GetComponentsInChildren<SpriteCropper>())
            {
                WriteCropResult(standing, cropper);
            }

            AssetDatabase.Refresh();
        }

        private static void WriteCropResult(UncroppedStanding standing, SpriteCropper cropper)
        {
            var uncropped = cropper.sprite.texture;
            var cropRect = cropper.cropRect;
            var cropped = new Texture2D(cropRect.width, cropRect.height, TextureFormat.RGBA32, false);
            var pixels = uncropped.GetPixels(cropRect.x, cropRect.y, cropRect.width, cropRect.height);
            cropped.SetPixels(pixels);
            cropped.Apply();
            var bytes = cropped.EncodeToPNG();
            var absoluteOutputFileName = Path.Combine(standing.absoluteOutputDirectory, cropper.sprite.name + ".png");
            Directory.CreateDirectory(Path.GetDirectoryName(absoluteOutputFileName));
            File.WriteAllBytes(absoluteOutputFileName, bytes);
        }

        private static void GenerateMetaData(UncroppedStanding standing)
        {
            foreach (var cropper in standing.GetComponentsInChildren<SpriteCropper>())
            {
                GenerateMetaData(standing, cropper);
            }
        }

        private static void GenerateMetaData(UncroppedStanding standing, SpriteCropper cropper)
        {
            var cropRect = cropper.cropRect;
            var uncropped = cropper.sprite.texture;
            var cropped =
                AssetDatabase.LoadAssetAtPath<Sprite>(Path.Combine(standing.outputDirectory,
                    cropper.sprite.name + ".png"));
            var meta = CreateInstance<SpriteWithOffset>();
            meta.offset = (cropRect.center - new Vector2(uncropped.width, uncropped.height) / 2.0f) /
                          cropper.sprite.pixelsPerUnit;
            meta.sprite = cropped;
            AssetDatabase.CreateAsset(meta, Path.Combine(standing.outputDirectory, cropper.sprite.name + ".asset"));
        }
    }
}