using System;
using System.Collections.Generic;

struct plane
{
    public int ID; //ID # of plane
    public string airline;
}

struct myQueue
{
    int numQueue; //# of planes in queue
    int front; //front of queue
    int rear; //rear of queue
}

public enum planeAction {arrive, depart};

class AirplaneSim
{
    public static Random rand = new Random();
    public static Queue<plane> landingQ = new Queue<plane>();
    public static Queue<plane> takeOffQ = new Queue<plane>();
    public static int planesProcesed=0;
    public static int currentTime;
    public static int evenTakeOffID=1;
    public static int oddLandingID=0;
    public static planeAction.depart depart;

    public static void Main ()
    {
        plane test;
        int numIncomingArrival = randomGenerator();
        int numTransToTakeOff = randomGenerator();

        for (int counter=numIncomingArrival; counter>=0; counter--) {
            NewPlane(depart);
        }
    }

    static int randomGenerator()
    {
        int num = rand.Next(0,4);
        return num;
    }

    void NewPlane (planeAction act) {
        plane p;
        planesProcesed ++;
        p.ID=planesProcesed;
        switch(act){
            case planeAction.arrive:
                Console.WriteLine("Plane ready to land");
                landingQ.Enqueue(p);
                break;
            case planeAction.depart:
                Console.WriteLine("Plane ready to take off");
                takeOffQ.Enqueue(p);
                break;
        }
    }
}
