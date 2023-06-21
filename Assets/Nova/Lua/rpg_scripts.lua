--- 引用Minigame.lua中的内容
function load_map(prefab_loader, map_name)
    if not check_lazy_before('minigame') then
        return
    end

    input_off()
    __Nova.coroutineHelper:StartInterrupt()

    show(prefab_loader, map_name)
    wait_fence()
    hide(prefab_loader)

    __Nova.coroutineHelper:StopInterrupt()
    input_on()
end
Nova.DialogueEntryPreprocessor.AddCheckpointNextPattern('minigame', 'ensure_ckpt_on_next_dialogue')


