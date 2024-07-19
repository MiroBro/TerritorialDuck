using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInfo : MonoBehaviour
{
    public Dictionary<Enums.PickupEffect, (string, string)> pickupsNameAndDesc = new()
    {
        { Enums.PickupEffect.CatBag, ("Handbag", "It's thick with things. You rummage through it and find treasure!")},
        { Enums.PickupEffect.DogBag, ("Backpack", "Someone dropped this fluffy, lumpy backpack. You waddle inside it and find treasures to power your annoyed quacking. \n + 30 Bread")},
        { Enums.PickupEffect.Beans, ("Bean", "You squash it with your beak and swallow. Very thick and moist. Your stomach rumbles threateningly.") },
        { Enums.PickupEffect.Bread, ("Bread piece", "Delicious and stolen. This bread fuels you, it eventually makes your quacks more powerfully annoying. \n \n + 1 Bread pieces") },
        { Enums.PickupEffect.Clover, ("Clover", "It's lush and green. You lean in, inhaling. An earthy scent overcomes you and your heart becomes happy. \n \n + 1 Luck") },
        { Enums.PickupEffect.DinoEggs, ("Dinosaur egg", "The feeling of your ancestor's might overcomes you, your resolve strengthens. \n \n A random stat recieves a permanent increase.") },
        { Enums.PickupEffect.Leaf1, ("Little leaf", "Cute and handy. You think you could use this to build stuff. \n\n + 1 Leaf") },
        { Enums.PickupEffect.Leaf2, ("Lush leaf", "Dazzling emerald in color. This would look beautiful around your pond. \n\n + 5 Leaves") },
        { Enums.PickupEffect.Leaf3, ("Ruby leaf", "Oh seven dieties of Quackia! This leaf makes your heart flutter. It's ruby red glimmers in the sun. \n\n + 10 Leaves") },
        { Enums.PickupEffect.Leaf4, ("Golden leaf", "Be still my heart. This leaf shimmers like the ocean och Beaktopia, like the giggles of ducklings. It will bless your pond. \n\n+ 100 Leaves") },
        { Enums.PickupEffect.LeafBlower, ("Leaf blower", "A human tool of legends with a fearsome roar. It blows like tornado winds.\n\nBlows all leaves to you.") },
        { Enums.PickupEffect.Loaf, ("Loaf", "To their demise, someone dropped a <i>whole</i> loaf of bread. Giga fuel for your loud quacks.\n\n+ 1 Bread pieces") },
        { Enums.PickupEffect.Megaphone, ("Megaphone", "It quivers as you hold it in your beak. You take a breath and <b> quack </b>!\n\nIncreases the decibels of your quacks so that all life on screen runs away.") },
        { Enums.PickupEffect.NailPolish, ("Nail polish", "You sit down and paint the tips of your webbed feet. You feel refreshed, relaxed  and cute. \n\n+ 1 Patience") },
        { Enums.PickupEffect.RainbowPolish, ("Rainbow nail polish", "With your butt planted on the soft grass you paint your webbed toes in magical rainbow colors. Feelings of zen wash over you. You look ducking adorable. \n\n+ 30 Patience") },
        { Enums.PickupEffect.RomanceNovel, ("Romance novel", "Your cheeks flush. Three ducks in a polyamorous relationship do unspeakable things to each other in this novella. You can't look away and you don't want to. As you flip through the scandalous pages it feels like time has stopped.\n\nStops time for 15 seconds") },
        { Enums.PickupEffect.Tea, ("Tea", "Vanilla tea with bean milk. Fallen baby feathers, this is <i> good </i>. Peacefullness gushes over you.\n\n+ 35 Patience") },
        { Enums.PickupEffect.Vaccuum, ("Vacuum", "It's a devilish device from the humans. It sounds like thunder and seems to suck objects into it. \n\nVacuums up all bread on the map.") },
    };

    public Enums.PickupEffect pickupEffect;
    public Sprite itemIcon;

    public string GetName()
    {
        return pickupsNameAndDesc[pickupEffect].Item1;
    }

    public string GetDescription()
    {
        return pickupsNameAndDesc[pickupEffect].Item2;
    }

}
