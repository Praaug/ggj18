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

        m_newGameViewModel = new NewGameViewModel(this);
        m_newGameViewModel.OnCloseNewGameCommand += NewGameViewModel_OnCloseNewGameCommand;
        m_newGameViewModel.OnStartGameCommand += NewGameViewModel_OnStartGameCommand;

        m_optionsViewModel = new OptionsViewModel(this);
        m_optionsViewModel.OnCloseOptionsCommand += OptionsViewModel_OnCloseOptionsCommand;

        m_syllablesInputViewModel = new SyllablesInputViewModel(this);
        m_syllablesInputViewModel.OnAcceptCommand += SyllablesViewModel_OnAcceptCommand;

        m_incommingTransmissionViewModel = new IncommingTransmissionViewModel();

        m_endScreenViewModel = new EndScreenViewModel();
        m_endScreenViewModel.OnOKCommand += EndScreenViewModel_OnOKCommand;

        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void EndScreenViewModel_OnOKCommand()
    {
        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void SyllablesViewModel_OnAcceptCommand()
    {
        CurrentDisplayedMenu = m_endScreenViewModel;
    }

    private void OptionsViewModel_OnCloseOptionsCommand()
    {
        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void NewGameViewModel_OnStartGameCommand()
    {
        throw new NotImplementedException();
    }

    private void NewGameViewModel_OnCloseNewGameCommand()
    {
        CurrentDisplayedMenu = m_mainViewModel;
    }

    private void MainViewModel_OnOpenOptionsCommand()
    {
        CurrentDisplayedMenu = m_optionsViewModel;
    }

    private void MainViewModel_OnOpenNewGameCommand()
    {
        CurrentDisplayedMenu = m_newGameViewModel;
    }

    private MainMenuViewModel m_mainViewModel;
    private OptionsViewModel m_optionsViewModel;
    private NewGameViewModel m_newGameViewModel;
    private SyllablesInputViewModel m_syllablesInputViewModel;
    private IncommingTransmissionViewModel m_incommingTransmissionViewModel;
    private EndScreenViewModel m_endScreenViewModel;


    private BaseViewModel m_currentDisplayedMenu;
}
