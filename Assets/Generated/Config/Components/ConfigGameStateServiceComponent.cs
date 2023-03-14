//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ConfigContext {

    public ConfigEntity gameStateServiceEntity { get { return GetGroup(ConfigMatcher.GameStateService).GetSingleEntity(); } }
    public GameStateServiceComponent gameStateService { get { return gameStateServiceEntity.gameStateService; } }
    public bool hasGameStateService { get { return gameStateServiceEntity != null; } }

    public ConfigEntity SetGameStateService(Game.IGameStateService newValue) {
        if (hasGameStateService) {
            throw new Entitas.EntitasException("Could not set GameStateService!\n" + this + " already has an entity with GameStateServiceComponent!",
                "You should check if the context already has a gameStateServiceEntity before setting it or use context.ReplaceGameStateService().");
        }
        var entity = CreateEntity();
        entity.AddGameStateService(newValue);
        return entity;
    }

    public void ReplaceGameStateService(Game.IGameStateService newValue) {
        var entity = gameStateServiceEntity;
        if (entity == null) {
            entity = SetGameStateService(newValue);
        } else {
            entity.ReplaceGameStateService(newValue);
        }
    }

    public void RemoveGameStateService() {
        gameStateServiceEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ConfigEntity {

    public GameStateServiceComponent gameStateService { get { return (GameStateServiceComponent)GetComponent(ConfigComponentsLookup.GameStateService); } }
    public bool hasGameStateService { get { return HasComponent(ConfigComponentsLookup.GameStateService); } }

    public void AddGameStateService(Game.IGameStateService newValue) {
        var index = ConfigComponentsLookup.GameStateService;
        var component = (GameStateServiceComponent)CreateComponent(index, typeof(GameStateServiceComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGameStateService(Game.IGameStateService newValue) {
        var index = ConfigComponentsLookup.GameStateService;
        var component = (GameStateServiceComponent)CreateComponent(index, typeof(GameStateServiceComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGameStateService() {
        RemoveComponent(ConfigComponentsLookup.GameStateService);
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
public sealed partial class ConfigMatcher {

    static Entitas.IMatcher<ConfigEntity> _matcherGameStateService;

    public static Entitas.IMatcher<ConfigEntity> GameStateService {
        get {
            if (_matcherGameStateService == null) {
                var matcher = (Entitas.Matcher<ConfigEntity>)Entitas.Matcher<ConfigEntity>.AllOf(ConfigComponentsLookup.GameStateService);
                matcher.componentNames = ConfigComponentsLookup.componentNames;
                _matcherGameStateService = matcher;
            }

            return _matcherGameStateService;
        }
    }
}