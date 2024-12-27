using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public enum GameState
    {
        running,
        menu,
        loading,
        win,
        end,
    }
    public enum MenuState
    {
        main,
        none,
        options,
        levelSelect,
        end,
    }
    public enum MessageType
    {
        tutorial,
        carCollision,
        parkInLine,
        grassCollision,
        nextLevel
    }
    public enum Sound
    {
        hit,
        gear_shift,
        idle,
        brake,
        theme,
    }

    public enum EntityType
    {
        player,
        car,
        steeringWheel,
        dummy,
        collisionZone,
        winZone,
        noParkZone,
    }
    public enum TileID
    {
        // street markings
        sCurbManhole = 0,
        sEmpty,
        sTwoLines,
        gGrass,    // grass is le here
        sYellow = 4,
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
        // grass, g for grass bro
        gEmpty,
        gLeftTopPath,
        gTopPath,
        gRightTopPath,
        gLeftSidePath,
        gRightSidePath,
        gLeftBottomPath,
        gBottomPath,
        gRightBottomPath,
        // ponnd l 
        lLake,
        lTopLeft,
        lTop1,
        lTop2,
        lTop3,
        lTopRight,
        lLeftSide1,
        lLeftSide2,
        lLeftSide3,
        lRightSide1,
        lRightSide2,
        lRightSide3,
        lLeftBottom,
        lBottom1,
        lBottom2,
        lBottom3,
        lRightBottom,

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
