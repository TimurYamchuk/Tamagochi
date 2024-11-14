using System;

public class Tamagotchi
{
    private static readonly string[] Requests = { "покормить", "погулять", "уложить спать", "полечить", "поиграть" };
    private static Random random = new Random();
    private string lastRequest = "";
    private int unfulfilledRequests = 0;
    private bool isSick = false;
    private bool isAlive = true;
    private int lifetimeSeconds = 60;
    public event Action<string> OnFeedRequest;
    public event Action<string> OnWalkRequest;
    public event Action<string> OnSleepRequest;
    public event Action<string> OnTreatRequest;
    public event Action<string> OnPlayRequest;

    public void StartLife()
    {
        DateTime endTime = DateTime.Now.AddSeconds(lifetimeSeconds);
        Console.WriteLine("Ваш Тамагочи начал свою жизнь!\n");

        while (isAlive && DateTime.Now < endTime)
        {
            string request = GenerateRequest();
            DisplayTamagotchi(request);

            Console.WriteLine($"Тамагочи просит: {request}. Удовлетворить? (да/нет)");
            string response = Console.ReadLine()?.ToLower();

            if (response == "да")
            {
                ProcessRequest(request, true);
            }
            else
            {
                ProcessRequest(request, false);
            }

            if (!isAlive) break;

            System.Threading.Thread.Sleep(2000);
        }

        if (isAlive)
            Console.WriteLine("Жизненный цикл Тамагочи завершён.");
    }

    private string GenerateRequest()
    {
        string request;
        do
        {
            request = Requests[random.Next(Requests.Length)];
        } while (request == lastRequest);

        lastRequest = request;
        return request;
    }

    private void DisplayTamagotchi(string request)
    {
        Console.Clear();
        Console.WriteLine(" (°-°) ");
        Console.WriteLine("/|_|\\ ");
        Console.WriteLine("  |  ");
        Console.WriteLine(" / \\ ");
        Console.WriteLine($"\nТамагочи: {(isSick ? "болеет" : "здоров")}");

        switch (request)
        {
            case "покормить":
                OnFeedRequest?.Invoke("Тамагочи просит покормить.");
                break;
            case "погулять":
                OnWalkRequest?.Invoke("Тамагочи просит погулять.");
                break;
            case "уложить спать":
                OnSleepRequest?.Invoke("Тамагочи просит уложить спать.");
                break;
            case "полечить":
                OnTreatRequest?.Invoke("Тамагочи просит полечить.");
                break;
            case "поиграть":
                OnPlayRequest?.Invoke("Тамагочи просит поиграть.");
                break;
        }

        Console.WriteLine($"Просьба: {request}");
    }

    private void ProcessRequest(string request, bool accepted)
    {
        if (accepted)
        {
            Console.WriteLine($"Вы выполнили просьбу: {request}.");
            if (isSick && request == "полечить")
            {
                isSick = false;
                unfulfilledRequests = 0;
                Console.WriteLine("Тамагочи выздоровел!");
            }
            else if (!isSick)
            {
                unfulfilledRequests = 0;
            }
        }
        else
        {
            Console.WriteLine($"Вы отказали в просьбе: {request}.");
            unfulfilledRequests++;

            if (unfulfilledRequests >= 3)
            {
                if (!isSick)
                {
                    isSick = true;
                    Console.WriteLine("Тамагочи заболел!");
                }
                else if (request == "полечить")
                {
                    isAlive = false;
                    Console.WriteLine("Тамагочи умер от болезни...");
                }
            }
        }
    }
}
