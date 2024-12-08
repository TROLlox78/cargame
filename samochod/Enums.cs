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
    }
    public enum TileID
    {
        // street markings
        sCurbManhole = 0,
        sEmpty,
        sTwoLinesH,
        sTwoLinesV,
        sBend,
        sYellowV,
        sYellowH,
        sStripSideR,
        sDoubleStripSide,
        sStripSideL,
        sManhole,
        sdoubleLane,
        sStraigthLine,
        
        // sidewalk, p for pavement
        pTopLeft,
        pTop1,
        pTop2,
        pTopRight,
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
        pBottomLeft,
        pBottom1,
        pBottom2,
        pBottomRight,
    }

}
