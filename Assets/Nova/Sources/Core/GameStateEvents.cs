using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Nova
{
    public class NodeChangedData
    {
        public readonly string newNode;

        public NodeChangedData(string newNode)
        {
            this.newNode = newNode;
        }
    }

    [Serializable]
    public class NodeChangedEvent : UnityEvent<NodeChangedData> { }

    public class DialogueChangedData
    {
        public readonly NodeRecord nodeRecord;
        public readonly long checkpointOffset;
        public readonly ReachedDialogueData dialogueData;
        public readonly DialogueDisplayData displayData;
        public readonly bool isReached;
        public readonly bool isReachedAnyHistory;

        public DialogueChangedData(NodeRecord nodeRecord, long checkpointOffset, ReachedDialogueData dialogueData,
            DialogueDisplayData displayData, bool isReached, bool isReachedAnyHistory)
        {
            this.nodeRecord = nodeRecord;
            this.checkpointOffset = checkpointOffset;
            this.dialogueData = dialogueData;
            this.displayData = displayData;
            this.isReached = isReached;
            this.isReachedAnyHistory = isReachedAnyHistory;
        }
    }

    [Serializable]
    public class DialogueChangedEvent : UnityEvent<DialogueChangedData> { }

    public class ChoiceOccursData
    {
        [ExportCustomType]
        public class Choice
        {
            public readonly Dictionary<SystemLanguage, string> texts;
            public readonly ChoiceImageInformation imageInfo;
            public readonly bool interactable;

            public Choice(Dictionary<SystemLanguage, string> texts, ChoiceImageInformation imageInfo,
                bool interactable)
            {
                this.texts = texts;
                this.imageInfo = imageInfo;
                this.interactable = interactable;
            }

            public Choice(string text, ChoiceImageInformation imageInfo, bool interactable) : this(
                new Dictionary<SystemLanguage, string> { [I18n.DefaultLocale] = text }, imageInfo, interactable)
            { }
        }

        public readonly IReadOnlyList<Choice> choices;

        public ChoiceOccursData(IReadOnlyList<Choice> choices)
        {
            this.choices = choices;
        }
    }

    [Serializable]
    public class ChoiceOccursEvent : UnityEvent<ChoiceOccursData> { }

    public class RouteEndedData
    {
        public readonly string endName;

        public RouteEndedData(string endName)
        {
            this.endName = endName;
        }
    }

    [Serializable]
    public class RouteEndedEvent : UnityEvent<RouteEndedData> { }
}
