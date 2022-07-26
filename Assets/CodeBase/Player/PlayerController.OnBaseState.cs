﻿using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class OnBaseState : PlayerStateBase
        {
            public OnBaseState(PlayerController player) : base(player)
            { }

            public override void Enter()
            {
                Controller.CanBeTarget = false;
                Controller.RemoveAllResourcesToBase();
                Controller._gun.Off();
                Controller._hPBar.SetState(false);
            }

            public override void Execute(float deltaTime)
            {
                Vector3 normalizedMovementVector = new Vector3(Controller._input.Axis.x, 0, Controller._input.Axis.y).normalized;

                if (normalizedMovementVector != Vector3.zero)
                {
                    Controller._mover.Move(normalizedMovementVector, deltaTime);
                    Controller._viewer.PlayRun();
                    Controller._rotator.RotateIn(normalizedMovementVector, deltaTime);
                }
                else
                {
                    Controller._viewer.PlayIdle();
                }
            }

            public override void Exit()
            {
                Controller.CanBeTarget = true;
                Controller._gun.On();
                Controller._hPBar.SetState(true);
            }

            protected override bool CheckNeedAndDoTransitions() => false;
        }
    }
}