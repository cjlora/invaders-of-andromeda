using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial013
{
    class Camera
    {
        //chuck
        public bool camera_stop_end_level = false;
        //--

        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 center;
        public Vector2 Center
        {
            get { return center; }
        }

        public Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, int xOfset, int yOffset)
        {
            //Console.WriteLine("viewport widht/hieght: " + viewport.Height);
            if (position.X < viewport.Width / 2) //after half of screen move
                center.X = viewport.Width / 2;
            else if (position.X > xOfset - (viewport.Width / 2)) //end of level
            {
                center.X = xOfset - (viewport.Width / 2);

                //chuck
                camera_stop_end_level = true;
                //--
            }
            else
                center.X = position.X;

            /*
            if (position.Y < viewport.Height / 4)
                center.Y = viewport.Height / 4;
            else if (position.Y > xOfset - (viewport.Height / 4))
                center.Y = xOfset - (viewport.Height / 4);
            else
                center.Y = position.Y;
            */


            /*transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                -center.Y + (viewport.Height / 2), 0));*/

            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                -center.Y , 0));
        }
    }
}
