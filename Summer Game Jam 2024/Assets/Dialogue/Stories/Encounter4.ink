#  Nausea
#Party true
VAR medicine = 0

 -> main
 
 === main ===
 One of your passengers is looking a little pale.
 "I feel sick, like I'm gonna puke." Knowing that the speed stomach of contents averages at about 30-40 cm/s, you realize the weight of the situation and have to act fast.
    + [Keep Driving]
        -> FirstChoice
    + [Pull Over.]
        -> SecondChoice
    + [Offer Medicine]
        -> ThirdChoice
        
== FirstChoice ==
#harmparty 20
They quickly open the window, letting their stomach loose into the wind. It's a gross thought to dwell on but at least none of it got into the car (the smell did though). (PARTY happiness -2)
-> DONE
== SecondChoice ==
#harmparty 10
You pull over to the side of the road, and not a second too soon as the next thing you hear is about 43 seconds of retching, 15 seconds of suppressed crying, a couple of emotes, and a "thug it out" to end on a high note. Back on the road! (PARTY happiness -1)

-> DONE
== ThirdChoice ==
#happyparty 20
#medicine -1
~medicine = -1
The boogie bomb in their stomach worked a little quicker than the digestive system versus some Pepto in the time it took for you to pull over, but it did help the aftermath. (PARTY happiness +2, medicine -1)


-> DONE