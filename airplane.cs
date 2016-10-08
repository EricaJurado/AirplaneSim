using System;
using System.Collections.Generic;

public struct plane
{
    public int ID; //ID # of plane
    public string airline; //name of airline

}

public enum planeAction {arrive, depart};

class Runway
{
    public static Queue<plane> landingQ = new Queue<plane>();
    public static Queue<plane> takeOffQ = new Queue<plane>();
    public static Random rand = new Random();
    public static int planesProcesed=0;
    public static int currentTime=12*60; //simulation starts at noon
    public static int timePassed=0;
    public static int takeOffID=0;
    public static int landingID=1;

    public static void Main ()
    {
        plane test;
        int numIncomingArrival = randomGenerator();
        int numTransToTakeOff = randomGenerator();

        for (int counterArrive=numIncomingArrival; counterArrive>0; counterArrive--) {
            NewPlane(planeAction.arrive);
        }

        for (int counterDepart=numTransToTakeOff; counterDepart>0; counterDepart--) {
            NewPlane(planeAction.depart);
        }

        Console.WriteLine("The time is: " + Time());
        Console.WriteLine("There are " + landingQ.Count + "planes waiting to land.");
        Console.WriteLine("There are " + takeOffQ.Count + "planes waiting to takeoff.");
    }

    static int randomGenerator()
    {
        int num = rand.Next(0,4);
        return num;
    }

    public static void NewPlane (planeAction action) {
        plane newPlane = new plane();
        newPlane.ID=planesProcesed;

        if (action == planeAction.arrive) {
            newPlane.ID = landingID;
            landingID +=2;
            landingQ.Enqueue(newPlane);
        } else if (action == planeAction.depart) {
            takeOffQ.Enqueue(newPlane);
            newPlane.ID = takeOffID;
            takeOffID +=2;
        }

        planesProcesed ++;

    }

    public static string Time(){
        return TimeSpan.FromMinutes(currentTime).ToString(@"hh\:mm");
    }
}
