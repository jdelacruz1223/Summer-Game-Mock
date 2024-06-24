#  Headache
#Party true
VAR medicine = 0

 -> main
 
 === main ===
 One of your passengers has one.
    + ["It's 'cause you always on that phone"]
        -> FirstChoice
    + [Offer Medicine]
        -> SecondChoice
    + [Offer Fish Bait]
        -> ThirdChoice
        
== FirstChoice ==
#harmparty 10
Your passenger groans in discomfort and tries to sleep off the headache, shuffling and shoving the others around carelessly. You'll never know if it was really motion sickness caused by being on that phone, but at least you think you're funny. (PARTY happiness -1)
-> DONE
== SecondChoice ==
#happyparty 10
#medicine -1
You give your passenger some Ibuprofen. Within a couple minutes, they let out a sigh of relief and continues to enjoy imagining a blue hedgehog running along the the hills and power poles (please tell me I'm not the only one). (PARTY happiness +1, medicine -1)
~medicine = -1
-> DONE
== ThirdChoice ==
#harm 20
..Why? (PARTY Happiness -2)
-> DONE