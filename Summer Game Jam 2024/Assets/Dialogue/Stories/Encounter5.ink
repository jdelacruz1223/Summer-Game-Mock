#  Rogue Cargo
#Party true

EXTERNAL setItem(item)

 -> main
 
 === main ===
 An F150 (truck) roars in front of you as it pulls ahead to your lane to pass you. As it merges, a belt goes flying, as well as the flatbed trunk door and a couple of cardboard boxes. 
    + [Tank it, run it over.]
        -> FirstChoice
    + [Swerve around.]
        -> SecondChoice
    + [Pull over and search the boxes.]
        -> ThirdChoice
        
== FirstChoice ==
~setItem("tires -1")
They're just cardboard, what's the worst that can happen? Torn underside shielding? Oil leak? Combustion? Nah, just a punctured tire. (tire -1)


-> DONE
== SecondChoice ==
#happyparty 10
You succeeded the quick time event (in your head) and successfully swerved out of the way of the rogue cargo! (PARTY happiness +1)


-> DONE
== ThirdChoice ==
#harmparty 10
#reward money
#reward medicine
#reward food
Pulling over to the boxes, you realizes a very familiar and very widely known shipping company brand logo on them. The truck is long gone, so I guess they won't be needing this stuff anymore. Although some us aren't too keen on "stealing". (PARTY happiness -1, money +, medicine +1, food +1)


-> DONE