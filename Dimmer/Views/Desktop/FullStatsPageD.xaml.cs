using Syncfusion.Maui.Toolkit.Chips;
using Syncfusion.Maui.Toolkit.EffectsView;

namespace Dimmer_MAUI.Views.Desktop;

public partial class FullStatsPageD : ContentPage
{
    public FullStatsPageD(HomePageVM homePageVM)
    {
        InitializeComponent();
        this.BindingContext = homePageVM;
        HomePageVM = homePageVM;
    }
    public HomePageVM HomePageVM { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        
        if (HomePageVM.TemporarilyPickedSong is null)
        {
            return;
        }
        HomePageVM.CurrentPage = PageEnum.FullStatsPage;
        //HomePageVM.ShowGeneralTopXSongsCommand.Execute(null);
        StatsTabs.SelectedItem = StatsTabs.Children[0];
        HomePageVM.LoadDailyData();
    }

    private async void StatsTabs_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.Chips.SelectionChangedEventArgs e)
    {
        var selectedTab = StatsTabs.SelectedItem;
        var send = (SfChipGroup)sender;
        var selected = send.SelectedItem as SfChip;
        if (selected is null)
        {
            return; 
        }
        _ = int.TryParse(selected.CommandParameter.ToString(), out int selectedStatView);        
        
        switch (selectedStatView)
        {
            case 0:
                HomePageVM.GetDaysNeededForNextEddington();
                HomePageVM.GetParetoPlayRatio();
                HomePageVM.GetGiniPlayIndex();
                HomePageVM.GetFibonacciPlayCount();
                
                //GeneralStatsView front, rest back
                break;
            case 1:
                //SongsStatsView front, rest back
                HomePageVM.GetLifetimeBingeSong();
                HomePageVM.GetBiggestClimbers();
                HomePageVM.GetMostDimmsPerDay(15);
                //HomePageVM.GetNotListenedStreaks();
                //HomePageVM.GetTopStreakTracks();

                //HomePageVM.GetGoldenOldies();


                //HomePageVM.GetBiggestFallers(DateTime.Now.Month, DateTime.Now.Year);
                //HomePageVM.GetStatisticalOutlierSongs();
                //HomePageVM.GetDailyListeningVolume();
                //HomePageVM.GetUniqueTracksInMonth(DateTime.Now.Month, DateTime.Now.Year);
                //HomePageVM.GetNewTracksInMonth(DateTime.Now.Month, DateTime.Now.Year);
                //HomePageVM.GetOngoingGapBetweenTracks();
                break;
            case 2:
                HomePageVM.GetTopPlayedArtists();       
                break;
            case 3:
                HomePageVM.GetTopPlayedAlbums();
                break;
            case 4:
                break;
            case 5:
                
                break;
            case 6:
                
                break;
            default:

                break;
        }

        var viewss = new Dictionary<int, View>
        {
            {0, GeneralStatsView},
            {1, SongsStatsView},
            {2, ArtistsStatsView},
            {3, AlbumsStatsView},
            {4, DimmsStatsView},
            {5, PlaylistsStatsView},
            {6, GenreStatsView }
        };
        if (!viewss.ContainsKey(selectedStatView))
            return;

        await Task.WhenAll
            (viewss.Select(kvp =>
            kvp.Key == selectedStatView
            ? kvp.Value.AnimateFadeInFront()
            : kvp.Value.AnimateFadeOutBack()));
        return;
    }

    int SelectedGeneralView;

    private void SfEffectsView_TouchUp(object sender, EventArgs e)
    {
        var send = (SfEffectsView)sender;
        int.TryParse(send.TouchUpCommandParameter.ToString(), out SelectedGeneralView);

        switch (SelectedGeneralView)
        {
            case 0:
                break;
            case 1:
                break;                
            case 2:
                break;
            case 3:
                break;                
            case 4:
                break;
            case 5:
                break;                
            case 6:
                break;
            case 7:
                break;                
            case 8:
                break;
            case 9:
                break;                
            case 10:
                break;
            case 11:
                break;
            default:
                break;
        }

    }


    private async void FocusModePointerRec_PEntered(object sender, PointerEventArgs e)
    {
        var send = (View)sender;
        await send.DimmIn(500);
     
    }
    private async void FocusModePointerRec_PExited(object sender, PointerEventArgs e)
    {
        var send = (View)sender;
        await send.DimmOut(300);
     
    }

    private void StatView_Loaded(object sender, EventArgs e)
    {
        var send = (View)sender;
        _ = send.DimmOut(300);
    }
}
