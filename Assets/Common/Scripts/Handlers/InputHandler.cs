using System.Collections.Generic;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance {get; private set;}
    public List<IInputBind> InputBinds {get; private set;}
    
    void Awake()
    {
        if (Instance == null ){ Instance = this; }
        
        InputBinds = new()
        {
            new Keybind<KeyCode>("MoveUp", new(){KeyCode.W}),
            new Keybind<KeyCode>("MoveDown", new(){KeyCode.S}),
            new Keybind<KeyCode>("MoveLeft", new(){KeyCode.A}),
            new Keybind<KeyCode>("MoveRight", new(){KeyCode.D}),
            new Keybind<KeyCode>("Escape", new(){KeyCode.Escape}),
            new Keybind<KeyCode>("Inventory", new(){KeyCode.I}),
            new Keybind<KeyCode>("SwapMode", new(){KeyCode.Tab}),
            new Keybind<KeyCode>("Boost", new(){KeyCode.LeftShift}),
            new Keybind<KeyCode>("Interact", new(){KeyCode.E}),
            new Keybind<KeyCode>("1", new(){KeyCode.Alpha1}),
            new Keybind<KeyCode>("2", new(){KeyCode.Alpha2}),
            new Keybind<KeyCode>("3", new(){KeyCode.Alpha3}),
            new Keybind<KeyCode>("4", new(){KeyCode.Alpha4}),
            new MouseBind<int>("Shoot", new(){0})
        };
    }
    
    void Update()
    {
        foreach (var bind in InputBinds)
        { 
            bind.Tick();  
        }
    }

    public IInputBind GetBindByName(string Name) => InputBinds.Find(x => x.GetName().Equals(Name));

    void ChangeBind<T>(InputBind<T> bind, InputBind<T> newBind)
    {
        RemoveBind(bind);
        AddBind(newBind);
    }
    
    void AddBind<T>(InputBind<T> bind) 
    {
        if (!InputBinds.Contains(bind))
        {
            InputBinds.Add(bind);
        }
    } 
    void RemoveBind<T>(InputBind<T> bind) 
    {
        if (InputBinds.Contains(bind))
        {
            InputBinds.Remove(bind);
        }
    }
}

public interface IInputBind 
{
    public abstract bool GetInputDown();
    public abstract bool GetInputUp();
    public abstract string GetName();
    public abstract bool GetHold();
    public abstract void Tick();
}
public abstract class InputBind<T> : IInputBind
{
    public List<T> Keys;
    public string Name {get; protected set;}
    public bool Hold {get; protected set;}
    public InputBind(string name, List<T> keys)
    {
        Keys = keys;
        Name = name;
    }
    public abstract void Tick();
    public string GetName() => Name;
    public virtual bool GetInputDown() => false;
    public virtual bool GetInputUp() => false;

    
    public bool GetHold() => Hold;
}
public class Keybind<T> : InputBind<T>
{
    public Keybind(string name, List<T> keys) : base(name, keys){}
    public override void Tick()
    {
        Hold = false;
        for (int k = 0; k < Keys.Count; k++)
        {
            if (Input.GetKey((KeyCode)(object)Keys[k]))
            {
                Hold = true;
                break;
            }   
        }
    }
    public override bool GetInputDown()
    {
        bool down = false;

        for (int k = 0; k < Keys.Count; k++)
        {
            if (Input.GetKeyDown((KeyCode)(object)Keys[k]))
            {
                down = true;
                break;
            }   
        }
        return down;
    }
    public override bool GetInputUp()
    {
        bool up = false;

        for (int k = 0; k < Keys.Count; k++)
        {
            if (Input.GetKeyUp((KeyCode)(object)Keys[k]))
            {
                up = true;
                break;
            }   
        }
        return up;
    }
}

public class MouseBind<T> : InputBind<T>
{
    public MouseBind(string name, List<T> keys) : base(name, keys){}
    public override void Tick()
    {
        Hold = false;
        for (int k = 0; k < Keys.Count; k++)
        {
            if (Input.GetMouseButton((int)(object)Keys[k]))
            {
                Hold = true;
                break;
            }   
        }
    }

    public override bool GetInputDown()
    {
        bool down = false;

        for (int k = 0; k < Keys.Count; k++)
        {
            if (Input.GetMouseButtonDown((int)(object)Keys[k]))
            {
                down = true;
                break;
            }   
        }
        return down;   
    }
    
    public override bool GetInputUp()
    {
        bool up = false;

        for (int k = 0; k < Keys.Count; k++)
        {
            if (Input.GetMouseButtonUp((int)(object)Keys[k]))
            {
                up = true;
                break;
            }   
        }
        return up;   
    }
}