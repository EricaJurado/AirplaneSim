using System;
using System.Collections;
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
    public static int planesProcesed = 0;
    public static int currentTime = 12*60; //simulation starts at noon
    public static int timePassed = 0;
    public static int takeOffID = 0;
    public static int landingID = 1;
    public static int planesRefused = 0;
    public static int planesLanded = 0;
    public static int planesTakenOff = 0;
    public static int totalWaitTimeLand = 0;
    public static int totalWaitTimeTakeOff = 0;


    public static void Main ()
    {
        int numRunways=2;

        while (timePassed < 120)
        {
            int numIncomingArrival = randomGenerator();
            int numTransToTakeOff = randomGenerator();

            Console.WriteLine("\nThe time is: " + Time());
            for (int counterArrive=numIncomingArrival; counterArrive>0; counterArrive--)
            {
                NewPlane(planeAction.arrive);
            }

            for (int counterDepart=numTransToTakeOff; counterDepart>0; counterDepart--)
            {
                NewPlane(planeAction.depart);
            }

            Console.WriteLine(numIncomingArrival + " planes requested to land.");
            Console.WriteLine(numTransToTakeOff + " planes requested to depart.");

            totalWaitTimeLand += landingQ.Count*5;
            totalWaitTimeTakeOff += takeOffQ.Count*5;

            manageRunways(numRunways);

            List<int> waitingToLand = new List<int>();
            List<int> waitingToDepart = new List<int>();
            IEnumerator eLanding = landingQ.GetEnumerator();
            IEnumerator eDeparting = takeOffQ.GetEnumerator();
            while (eLanding.MoveNext())
            {
                plane current = (plane)eLanding.Current;
                waitingToLand.Add(current.ID);
            }
            while (eDeparting.MoveNext())
            {
                plane current = (plane)eDeparting.Current;
                waitingToDepart.Add(current.ID);
            }
            Console.WriteLine("Planes waiting to land: {0}", string.Join(", ", waitingToLand));
            Console.WriteLine("Planes waiting to depart: {0}", string.Join(", ", waitingToDepart));


            timePassed += 5;
            currentTime += 5;

        }

        totalWaitTimeLand -= landingQ.Count*5;
        totalWaitTimeTakeOff -= takeOffQ.Count*5;

        int averageWaitLand = totalWaitTimeLand/planesLanded;
        int averageWaitTakeOff = totalWaitTimeTakeOff/planesTakenOff;

        Console.WriteLine("\n\n\n\nSummary statistics: ");
        Console.WriteLine("Planes processed: " + planesProcesed);
        Console.WriteLine("Planes landed: " + planesLanded);
        Console.WriteLine("Planes taken off: " + planesTakenOff);
        Console.WriteLine("# of planes refused: " + planesRefused);
        Console.WriteLine("Average wait time to land: " + averageWaitLand);
        Console.WriteLine("Average wait time to take off: " + averageWaitTakeOff);
    }

    public static void NewPlane (planeAction action)
    {
        plane newPlane = new plane();
        newPlane.ID=planesProcesed;
        newPlane.airline=airlineName();

        if (action == planeAction.arrive)
        {
            newPlane.ID = landingID;
            landingID +=2;
        }
        else if (action == planeAction.depart)
        {
            newPlane.ID = takeOffID;
            takeOffID +=2;
        }

        bool refused = checkRefuse(newPlane, action);
        if (!refused && action == planeAction.arrive)
        {
            landingQ.Enqueue(newPlane);
        }
        else if (!refused && action == planeAction.depart)
        {
            takeOffQ.Enqueue(newPlane);
        }

        planesProcesed ++;

    }

    static string airlineName()
    {
        string[] names = {"American", "United", "Southwest", "Delta", "Frontier"};
        int index = rand.Next(0,5);
        return names[index];
    }

    public static bool checkRefuse (plane newPlane, planeAction action)
    {
        if (landingQ.Count >= 5 && action == planeAction.arrive)
        {
            Console.WriteLine("Plane " + newPlane.ID + " was turned away because there were too many planes waiting to land.");
            planesRefused ++;
            return true;
        }
        else if (takeOffQ.Count >= 5 && action != planeAction.depart)
        {
            Console.WriteLine("Plane " + newPlane.ID + " was turned away because there were too many planes waiting to take off.");
            planesRefused ++;
            return true;
        }

        return false;

    }

    static void manageRunways(int runways)
    {
        for (int countRunways=1; countRunways<runways+1; countRunways++)
        {
            if (landingQ.Count >= takeOffQ.Count && landingQ.Count != 0 )
            {
                plane test = landingQ.Dequeue();
                Console.WriteLine(test.airline + "'s plane, plane #" + test.ID + ", was cleared to land on runway " + countRunways + ".");
                planesLanded++;
            }
            else if (takeOffQ.Count != 0 )
            {
                plane test = takeOffQ.Dequeue();
                Console.WriteLine(test.airline + "'s plane, plane #" + test.ID + ", was cleared to takeoff on runway " + countRunways + ".");
                planesTakenOff++;
            }
        }
    }

    static int randomGenerator()
    {
        int num = rand.Next(0,4);
        return num;
    }

    public static string Time()
    {
        return TimeSpan.FromMinutes(currentTime).ToString(@"hh\:mm");
    }
}
