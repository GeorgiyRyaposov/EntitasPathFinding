//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GridsCellPositionListenerComponent gridsCellPositionListener { get { return (GridsCellPositionListenerComponent)GetComponent(GameComponentsLookup.GridsCellPositionListener); } }
    public bool hasGridsCellPositionListener { get { return HasComponent(GameComponentsLookup.GridsCellPositionListener); } }

    public void AddGridsCellPositionListener(System.Collections.Generic.List<IGridsCellPositionListener> newValue) {
        var index = GameComponentsLookup.GridsCellPositionListener;
        var component = (GridsCellPositionListenerComponent)CreateComponent(index, typeof(GridsCellPositionListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGridsCellPositionListener(System.Collections.Generic.List<IGridsCellPositionListener> newValue) {
        var index = GameComponentsLookup.GridsCellPositionListener;
        var component = (GridsCellPositionListenerComponent)CreateComponent(index, typeof(GridsCellPositionListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGridsCellPositionListener() {
        RemoveComponent(GameComponentsLookup.GridsCellPositionListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherGridsCellPositionListener;

    public static Entitas.IMatcher<GameEntity> GridsCellPositionListener {
        get {
            if (_matcherGridsCellPositionListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GridsCellPositionListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGridsCellPositionListener = matcher;
            }

            return _matcherGridsCellPositionListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddGridsCellPositionListener(IGridsCellPositionListener value) {
        var listeners = hasGridsCellPositionListener
            ? gridsCellPositionListener.value
            : new System.Collections.Generic.List<IGridsCellPositionListener>();
        listeners.Add(value);
        ReplaceGridsCellPositionListener(listeners);
    }

    public void RemoveGridsCellPositionListener(IGridsCellPositionListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gridsCellPositionListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGridsCellPositionListener();
        } else {
            ReplaceGridsCellPositionListener(listeners);
        }
    }
}