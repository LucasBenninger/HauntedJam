using Godot;
using System;

public class Player : KinematicBody2D
{
    bool isWalking = false;
    [Export] public int speed = 100;
    Vector2 velocity = new Vector2();

    AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        animationPlayer.Play("idle");
    }

    public override void _PhysicsProcess(float delta)
    {
        controls();
        look();
        move();
    }

    public void controls()
    {
        velocity = new Vector2();

        bool up = Input.IsActionPressed("w");
        bool down = Input.IsActionPressed("s");
        bool left = Input.IsActionPressed("a");
        bool right = Input.IsActionPressed("d");

        //Movement
        if (up)
        {
            velocity = new Vector2(speed, 0).Rotated(Rotation);
        }
        if (down)
        {
            velocity = new Vector2(-speed, 0).Rotated(Rotation);
        }
        // if (left)
        // {
        //     velocity = new Vector2(-speed, 0);
        // }
        // if (right)
        // {
        //     velocity = new Vector2(speed, 0);
        // }

        velocity = velocity.Normalized() * speed;
        //Animation

        if (up || down)
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

        //moveDir.y = -(up ? 1 : 0) + (down ? 1 : 0);
        //moveDir.x = -(left ? 1 : 0) + (right ? 1 : 0);
    }

    public void move()
    {
        this.MoveAndSlide(velocity, new Vector2(0, 0));
    }

    public void look()
    {
        this.LookAt(GetGlobalMousePosition());
    }
}
