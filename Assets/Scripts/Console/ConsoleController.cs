using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;


public class ConsoleController : MonoBehaviour, ILogHandler
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text logText;
    [SerializeField] private Button toggleButton;
    [SerializeField] private InputActionReference toggleConsoleInput;
    [SerializeField] private ScrollRect scrollRect;

    private Dictionary<string, IConsoleCommand> _commands = new();
    private Dictionary<string, string> _aliases = new();
    private ILogHandler _originalLogHandler;

    private void Awake()
    {
        toggleButton.onClick.AddListener(ToggleConsole);
        Application.logMessageReceived += HandleUnityLog;
        _originalLogHandler = Debug.unityLogger.logHandler;
        Debug.unityLogger.logHandler = this;

        RegisterCommand(new HelpCommand(this));
        RegisterCommand(new AliassesCommand(this));
        RegisterCommand(new PlayAnimationCommand(this));

        panel.SetActive(false);
    }

    public void RegisterCommand(IConsoleCommand cmd)
    {
        _commands[cmd.Name] = cmd;
        foreach (var alias in cmd.Aliases)
            _aliases[alias] = cmd.Name;
    }

    public IConsoleCommand GetCommand(string name)
    {
        if (_commands.TryGetValue(name, out var cmd))
            return cmd;
        if (_aliases.TryGetValue(name, out var aliasName))
            return _commands[aliasName];
        return null;
    }

    public void OnInputSubmit()
    {
        var text = inputField.text;
        inputField.text = string.Empty;
        ExecuteCommand(text);

        // originally, this were used to force the cursor to be on the console
        // but then, there were a lot of "unknown command" each time
        // the console was not focused
        //inputField.ActivateInputField();
        //inputField.Select();
    }

    private void ExecuteCommand(string line)
    {
        var parts = line.Split(' ');
        if (parts.Length == 0) return;

        var cmd = GetCommand(parts[0]);
        if (cmd == null)
        {
            Log($"Unknown command: {parts[0]}");
            return;
        }

        cmd.Execute(parts[1..]);
    }


    public void Log(string message)
    {
        logText.text += $"\n> {message}";

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private void ToggleConsole()
        => panel.SetActive(!panel.activeSelf);

    public void LogFormat(LogType logType, Object context, string format, params object[] args)
        => _originalLogHandler.LogFormat(logType, context, format, args);

    public void LogException(System.Exception exception, Object context)
        => _originalLogHandler.LogException(exception, context);

    private void HandleUnityLog(string condition, string stackTrace, LogType type)
        => Log(condition);

    private void OnEnable()
    {
        toggleConsoleInput?.action?.Enable();
        toggleConsoleInput.action.performed += HandleToggleInput;
    }

    private void OnDisable()
    {
        toggleConsoleInput?.action?.Disable();
        toggleConsoleInput.action.performed -= HandleToggleInput;
    }

    private void HandleToggleInput(InputAction.CallbackContext context)
    {
        ToggleConsole();
    }
}