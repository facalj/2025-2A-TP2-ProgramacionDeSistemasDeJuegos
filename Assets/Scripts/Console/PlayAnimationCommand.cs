using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationCommand : IConsoleCommand
{
    public string Name => "playanimation";
    public string Description => "Plays an animation on all present characters.";
    public IEnumerable<string> Aliases => new[] { "anim", "pa", "play" };

    private readonly ConsoleController _console;

    public PlayAnimationCommand(ConsoleController console)
        => _console = console;

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.Log("Use: playanimation [animation name]");
            return;
        }

        string animationName = args[0];
        var animators = GameObject.FindObjectsByType<Animator>(FindObjectsSortMode.None);
        int count = 0;
        var validAnimations = new HashSet<string>();

        foreach (var anim in animators.Where(a => a.runtimeAnimatorController != null))
        {
            var clips = anim.runtimeAnimatorController.animationClips;

            foreach (var name in clips.Select(c => c.name))
                validAnimations.Add(name);

            if (clips.Any(c => string.Equals(c.name, animationName, StringComparison.OrdinalIgnoreCase)))
            {
                anim.Play(animationName);
                count++;
            }
        }

        if (count > 0)
        {
            _console.Log($"Playing '{animationName}' on {count} characters.");
        }
        else
        {
            var list = string.Join(", ", validAnimations.OrderBy(n => n));
            _console.Log($"Animation '{animationName}' not found. Available animations: {list}");
        }
    }
}