using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Egz2024CzerwiecWPF // Nie chcą mi działać zdjęcia nie mam pojęcia czemi, są zawarte tu lokalnie w folderze Imagess
{
    public partial class MainWindow : Window
    {
        GraWKosci graWKosci = new GraWKosci();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRzucKoscmi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Rzucanie kośćmi
                graWKosci.losujWszystko();

                // Wyświetlanie obrazków dla każdej kości
                imgKoscPierwsza.Source = graWKosci.wyswietlObraz(graWKosci.TablicaWynikow[0]);
                imgKoscDruga.Source = graWKosci.wyswietlObraz(graWKosci.TablicaWynikow[1]);
                imgKoscTrzecia.Source = graWKosci.wyswietlObraz(graWKosci.TablicaWynikow[2]);
                imgKoscCzwarta.Source = graWKosci.wyswietlObraz(graWKosci.TablicaWynikow[3]);
                imgKoscPiata.Source = graWKosci.wyswietlObraz(graWKosci.TablicaWynikow[4]);

                // Wyświetlanie wyników
                lblWynikLosowania.Content = $"Wynik losowania: {graWKosci.iloscPunktow}";
                lblWynikCalkowity.Content = $"Suma punktów gry: {graWKosci.iloscPunktowCalaGra}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class GraWKosci
    {
        public const int ILOSC_KOSCI = 5;
        public const int ILOSC_SCIAN_KOSCI = 6;
        public const int ILOSC_NIEZNANA = -1;

        public static readonly string PLIK_KOSCI_JEDEN = "Images/DiceOne.jpeg";
        public static readonly string PLIK_KOSCI_DWA = "Images/DiceTwo.jpeg";
        public static readonly string PLIK_KOSCI_TRZY = "Images/DiceThree.jpeg";
        public static readonly string PLIK_KOSCI_CZTERY = "Images/DiceFour.jpeg";
        public static readonly string PLIK_KOSCI_PIEC = "Images/DiceFive.jpeg";
        public static readonly string PLIK_KOSCI_SZESC = "Images/DiceSix.jpeg";
        public static readonly string PLIK_KOSCI_NIEZNANY = "Images/DiceUnknown.jpeg";

        public int[] TablicaWynikow { get; private set; }
        public int[] TablicaPowtorzen { get; private set; }
        public int iloscPunktow { get; private set; }
        public int iloscPunktowCalaGra { get; private set; }

        public GraWKosci()
        {
            TablicaWynikow = new int[ILOSC_KOSCI];
            TablicaPowtorzen = new int[ILOSC_SCIAN_KOSCI + 1];
        }

        private int losujPojedynczy()
        {
            Random losowa = new Random();
            return losowa.Next(1, ILOSC_SCIAN_KOSCI + 1);
        }

        private void obliczWyniki(int[] tablicaRzutow)
        {
            iloscPunktow = 0;

            for (int i = 1; i <= ILOSC_SCIAN_KOSCI; i++)
            {
                if (tablicaRzutow[i] > 1)
                {
                    iloscPunktow += tablicaRzutow[i] * i;
                }
            }

            iloscPunktowCalaGra += iloscPunktow;
        }

        public void losujWszystko()
        {
            // Zerowanie tablicy powtórzeń
            for (int i = 0; i <= ILOSC_SCIAN_KOSCI; i++)
            {
                TablicaPowtorzen[i] = 0;
            }

            // Losowanie wyników rzutów
            for (int i = 0; i < ILOSC_KOSCI; i++)
            {
                TablicaWynikow[i] = losujPojedynczy();
                TablicaPowtorzen[TablicaWynikow[i]]++;
            }

            // Obliczanie wyniku
            obliczWyniki(TablicaPowtorzen);
        }

        public BitmapImage wyswietlObraz(int iloscOczek)
        {
            string sciezka;
            switch (iloscOczek)
            {
                case 1: sciezka = PLIK_KOSCI_JEDEN; break;
                case 2: sciezka = PLIK_KOSCI_DWA; break;
                case 3: sciezka = PLIK_KOSCI_TRZY; break;
                case 4: sciezka = PLIK_KOSCI_CZTERY; break;
                case 5: sciezka = PLIK_KOSCI_PIEC; break;
                case 6: sciezka = PLIK_KOSCI_SZESC; break;
                default: sciezka = PLIK_KOSCI_NIEZNANY; break;
            }

            return new BitmapImage(new Uri(Environment.CurrentDirectory + sciezka));
        }
    }
}
