using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Model
{
    public class SpriteModel
    {
        public Sprite sprite { get; set; }
        public AnimatorOverrideController animator { get; set; }
    }
}
