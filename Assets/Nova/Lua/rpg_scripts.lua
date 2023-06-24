-- 显示地图
function show_map(name)
    set_box('hide')

    --input_off()
    show(__Nova.mapPrefabLoader, name)
    minigame(__Nova.emptyLoader, 'Empty')
    set_box()
    --input_on()
end
