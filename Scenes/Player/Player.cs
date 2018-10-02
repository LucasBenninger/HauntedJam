using Godot;
using System;

public class Player : KinematicBody2D
{
    bool isWalking = false;
    int speed = 100;
    Vector2 moveDir = new Vector2();

    AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        animationPlayer.Play("idle");
    }

    public override void _PhysicsProcess(float delta)
    {
        controls();
        move();
        look();
    }

    public void controls()
    {

        bool up = Input.IsActionPressed("w");
        bool down = Input.IsActionPressed("s");
        bool left = Input.IsActionPressed("a");
        bool right = Input.IsActionPressed("d");

        if (up || down || left || right)
        {
            if (!isWalking)
            {
                animationPlayer.Play("walk");
                isWalking = true;
            }
        }
        else if (!up && !down && !left && !right && isWalking)
        {
            //Idle
            animationPlayer.Play("idle");
            isWalking = false;
        }

        moveDir.y = -(up ? 1 : 0) + (down ? 1 : 0);
        moveDir.x = -(left ? 1 : 0) + (right ? 1 : 0);
    }

    public void move()
    {
        Vector2 motion = new Vector2(moveDir.x * speed, moveDir.y * speed);
        this.MoveAndSlide(motion, new Vector2(0, 0));
    }

    public void look()
    {
        this.LookAt(GetGlobalMousePosition());
    }
}
