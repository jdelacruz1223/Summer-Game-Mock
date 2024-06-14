-> main

=== main ===
Hello traveler, what do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")

=== chosen(pokemon) ===
#reward medkit
You got {pokemon}!
-> END