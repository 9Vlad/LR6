using System;

class Viewer
{
    private int viewerNumber;

    public int ViewerNumber
    {
        get { return viewerNumber; }
    }

    public Viewer(int number)
    {
        viewerNumber = number;
    }
}

class Cinema
{
    private int totalSeats;
    private int occSeats;
    public delegate void EventHandler();
    public event EventHandler NotPlaces;

    public Cinema(int seats, string filmName)
    {
        totalSeats = seats;
    }

    public void PushViewer(Viewer viewer)
    {
        occSeats++;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Глядач {viewer.ViewerNumber} зайняв своє місце.");

        if (occSeats == totalSeats)
        {
            NotPlaces.Invoke();
        }
    }
}

class Security
{
    public void CloseZal()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Дежурний закрив зал.");
        SwitchOff.Invoke();
    }

    public event Cinema.EventHandler SwitchOff;
}

class Light
{
    public void Turn()
    {
        Console.WriteLine("Вимикаємо світло!");
        Begin.Invoke();
    }

    public event Cinema.EventHandler Begin;
}

class Hardware
{
    private string filmName;

    public Hardware(string film)
    {
        filmName = film;
    }

    public void FilmOn()
    {
        Console.WriteLine($"Починається фільм {filmName}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введіть кількість місць у залі:");
        int seats = int.Parse(Console.ReadLine());

        Console.WriteLine("Введіть назву фільму:");
        string filmName = Console.ReadLine();

        Cinema cinema = new Cinema(seats, filmName);
        Security security = new Security();
        Light light = new Light();
        Hardware hardware = new Hardware(filmName);

        cinema.NotPlaces += security.CloseZal;
        security.SwitchOff += light.Turn;
        light.Begin += hardware.FilmOn;

        for (int i = 1; i <= seats; i++)
        {
            Viewer viewer = new Viewer(i);
            cinema.PushViewer(viewer);
        }
    }
}
