using System;

class Program
{
    static void Main()
    {
        Tamagotchi tamagotchi = new Tamagotchi();

        FeedRequestHandler feedHandler = new FeedRequestHandler();
        WalkRequestHandler walkHandler = new WalkRequestHandler();
        SleepRequestHandler sleepHandler = new SleepRequestHandler();
        TreatRequestHandler treatHandler = new TreatRequestHandler();
        PlayRequestHandler playHandler = new PlayRequestHandler();

        tamagotchi.OnFeedRequest += feedHandler.HandleFeedRequest;
        tamagotchi.OnWalkRequest += walkHandler.HandleWalkRequest;
        tamagotchi.OnSleepRequest += sleepHandler.HandleSleepRequest;
        tamagotchi.OnTreatRequest += treatHandler.HandleTreatRequest;
        tamagotchi.OnPlayRequest += playHandler.HandlePlayRequest;

        tamagotchi.StartLife();
    }
}
