using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
   public enum EntityType
    {
        player,
        car,
        steeringWheel,
        dummy,
        collisionZone,
        winZone,
    }
    public enum TileID
    {
        // street markings
        sCurbManhole = 0,
        sEmpty,
        sTwoLines,
        sBend,
        sYellow,
        sStripSide,
        sDoubleStripSide,
        sManhole,
        sCrossing,
        sStraigthLine,
        
        // sidewalk, p for pavement
        pTopCorner,
        pTop1,
        pTop2,
        pBalls,
        pLeftSide,
        pTopShaded,
        pBottomShaded,
        pRightSide,
        pEmpty,
        pLeftSide2,
        pLeftShaded,
        pRightShaded,
        pRightSide2,
        pEmpty2,
        pBottomCorner,
        pBottom1,
        pBottom2,
    }
    public enum Model
    {
        Player,
        Sports,
        Muscle,
        Luxury,
        Limo,
        Police,
        Taxi,
        Ambulance,
        FireTruck,
        Pickup,
        Bones,
        Sedan,
        Transport,
        Transport2,
        Transport3,
        Transport4,
        Transport5,
        Transport6,

    }
    public enum ActionType { 
        None,Tile,
        Car,
        Entity,
        CollisionZone,
        WinZone,
    }


}
