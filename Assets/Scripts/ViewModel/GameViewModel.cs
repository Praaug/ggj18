using System;

public class GameViewModel : BaseViewModel
{
    public static GameViewModel instance { get; } = new GameViewModel();
    public override MenuEnum MenuType { get { throw new InvalidOperationException(); } }

    public event Action<BaseViewModel> OnUpdateMenu;

    public BaseViewModel CurrentDisplayedMenu
    {
        get
        {
            return m_currentDisplayedMenu;
        }
        private set
        {
            if (m_currentDisplayedMenu == value)
            {
                return;
            }

            m_currentDisplayedMenu = value;
            OnUpdateMenu?.Invoke(m_currentDisplayedMenu);
        }
    }

    public MainMenuViewModel MainMenuViewModel => m_mainViewModel;
    public OptionsViewModel OptionsViewModel => m_optionsViewModel;
    public NewGameViewModel NewGameViewModel => m_newGameViewModel;
    public SyllablesInputViewModel SyllablesInputViewModel => m_syllablesInputViewModel;
    public IncommingTransmissionViewModel IncommingTransmissionViewModel => m_incommingTransmissionViewModel;
    public EndScreenViewModel EndScreenViewModel => m_endScreenViewModel;

    public GameViewModel()
    {
        m_mainViewModel = new MainMenuViewModel(this);
        m_mainViewModel.OnOpenNewGameCommand += MainViewModel_OnOpenNewGameCommand;
        m_mainViewModel.OnOpenOptionsCommand += MainViewModel_OnOpenOptionsCommand;
        m_mainViewModel.OnLoadGameCommand += MainViewModel_OnLoadGameCommand;

        m_newGameViewModel = new NewGameViewModel(this);
        m_newGameViewModel.OnCloseNewGameCommand += NewGameViewModel_OnCloseNewGameCommand;
        m_newGameViewModel.OnStartGameCommand += NewGameViewModel_OnStartGameCommand;
        m_newGameViewModel.OnDisplayDurationChange += NewGameViewModel_OnDisplayDurationChange;
        m_newGameViewModel.OnPlayerCountChange += NewGameViewModel_OnPlayerCountChange;
        m_newGameViewModel.OnSessionNameChange += NewGameViewModel_OnSessionNameChange;

        m_optionsViewModel = new OptionsViewModel(this);
        m_optionsViewModel.OnCloseOptionsCommand += OptionsViewModel_OnCloseOptionsCommand;

        m_syllablesInputViewModel = new SyllablesInputViewModel(this);
        m_syllablesInputViewModel.OnAcceptCommand += SyllablesViewModel_OnAcceptCommand;

        m_incommingTransmissionViewModel = new IncommingTransmissionViewModel(this);
        m_incommingTransmissionViewModel.OnWaitTimePassed += IncommingTransmissionViewModel_OnWaitTimePassed;

        m_endScreenViewModel = new EndScreenViewModel();
        m_endScreenViewModel.OnOKCommand += EndScreenViewModel_OnOKCommand;

        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void IncommingTransmissionViewModel_OnWaitTimePassed()
    {
        CurrentDisplayedMenu = m_syllablesInputViewModel;
    }

    private void EndScreenViewModel_OnOKCommand()
    {
        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void SyllablesViewModel_OnAcceptCommand()
    {
        // Submit round to model
        GameManager.instance.SubmitRound();

        // Update UI
        CurrentDisplayedMenu = m_endScreenViewModel;
    }

    private void OptionsViewModel_OnCloseOptionsCommand()
    {
        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void NewGameViewModel_OnStartGameCommand()
    {
        // Start the game in the game manager
        GameManager.instance.StartNewGame();

        // Update the ui to show relvant game data
        CurrentDisplayedMenu = m_incommingTransmissionViewModel;
    }

    private void NewGameViewModel_OnCloseNewGameCommand()
    {
        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void NewGameViewModel_OnPlayerCountChange(int playerCount)
    {
        SessionParameters currentParameter = GameManager.instance.GetParameter();
        currentParameter.RoundCount = (byte)playerCount;
        GameManager.instance.SetParameter(currentParameter);
    }

    private void NewGameViewModel_OnDisplayDurationChange(float displayDuration)
    {
        SessionParameters currentParameter = GameManager.instance.GetParameter();
        currentParameter.DisplayDuration = displayDuration;
        GameManager.instance.SetParameter(currentParameter);
    }

    private void NewGameViewModel_OnSessionNameChange(string sessionName)
    {
        SessionParameters currentParameter = GameManager.instance.GetParameter();
        currentParameter.SessionName = sessionName;
        GameManager.instance.SetParameter(currentParameter);
    }

    private void MainViewModel_OnOpenOptionsCommand()
    {
        CurrentDisplayedMenu = m_optionsViewModel;
    }

    private void MainViewModel_OnOpenNewGameCommand()
    {
        CurrentDisplayedMenu = m_newGameViewModel;
    }

    private void MainViewModel_OnLoadGameCommand(int index)
    {
        GameManager.instance.LoadGame(index);

        // Update the ui to show relvant game data
        CurrentDisplayedMenu = m_incommingTransmissionViewModel;
    }

    private MainMenuViewModel m_mainViewModel;
    private OptionsViewModel m_optionsViewModel;
    private NewGameViewModel m_newGameViewModel;
    private SyllablesInputViewModel m_syllablesInputViewModel;
    private IncommingTransmissionViewModel m_incommingTransmissionViewModel;
    private EndScreenViewModel m_endScreenViewModel;


    private BaseViewModel m_currentDisplayedMenu;
}
