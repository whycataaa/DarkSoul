using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class UIType
    {
        public string Name{get;private set;}
        public string Path{get;private set;}

        public UIType(string _path)
        {
            Path=_path;
            Name=_path.Substring(_path.LastIndexOf("/")+1);
        }

    }
}
