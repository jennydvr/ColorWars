﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorWars
{
    class StarCatch : BlendingBehavior
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StarCatch(Character owner)
            : base()
        {
            // Create the blending behaviors
            BehaviorAndWeight a = new BehaviorAndWeight(new LookWhereYoureGoing(), 1);
            BehaviorAndWeight b = new BehaviorAndWeight(new FollowPath(), 1);
            BehaviorAndWeight c = new BehaviorAndWeight(new AvoidBullets(), 1);
            BehaviorAndWeight d = new BehaviorAndWeight(new ObstacleAvoidance(), 0.5f);
            BehaviorAndWeight e = new BehaviorAndWeight(new FriendsAvoidance(owner), 1);

            // Add them to the list
            blending.behaviors.Add(a);
            blending.behaviors.Add(b);
            blending.behaviors.Add(c);
            blending.behaviors.Add(d);
            blending.behaviors.Add(e);
        }
        
        #endregion

        #region Methods

        protected override void Update()
        {
            base.Update();
            
            // The follow needs a target
            blending.behaviors[1].behavior.Initialize(character, target);
        }

        #endregion
    }
}