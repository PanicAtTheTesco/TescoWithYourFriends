using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesco.Managers {
    public class EventManager {

        public static Action<Movement> ballScoreEvent;
        public static Action<Movement> ballStrokedOutEvent;
        public static Action resetBallsEvent;
        public static Action<Movement> ballHitEvent;
        public static Action<Movement> checkStrokeCountEvent;

        public static void StrokeOut(Movement movement) {
            if(ballStrokedOutEvent != null) {
                ballStrokedOutEvent.Invoke(movement);
            }
        }

        public static void CheckStrokes(Movement movement) {
            if(checkStrokeCountEvent != null) {
                checkStrokeCountEvent.Invoke(movement);
            }
        }

        public static void HitBall(Movement movement) {
            if(ballHitEvent != null) {
                ballHitEvent.Invoke(movement);
            }
        }

        public static void ResetBalls() {
            if(resetBallsEvent != null) {
                resetBallsEvent.Invoke();
            }
        }

        public static void BallScored(Movement move) {
            if(ballScoreEvent != null) {
                ballScoreEvent.Invoke(move);
            }
        }
    }
}
