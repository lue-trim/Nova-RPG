anim_hold_has_begun = false

function ensure_ckpt_on_next_dialogue()
    __Nova.checkpointHelper:EnsureCheckpointOnNextDialogue()
end

function anim_hold_begin()
    anim_hold:stop()
    __Nova.checkpointHelper:RestrainCheckpoint(Nova.CheckpointHelper.WarningStepsFromLastCheckpoint, true)
    anim_hold_has_begun = true
end
Nova.DialogueEntryPreprocessor.AddCheckpointPattern('anim_hold_begin', 'ensure_ckpt_on_next_dialogue')

function anim_hold_end()
    anim_hold:stop()
    __Nova.checkpointHelper:RestrainCheckpoint(0, true)
    anim_hold_has_begun = false
end

-- Use after something important happens
function update_global_save()
    __Nova.checkpointHelper:UpdateGlobalSave()
end
