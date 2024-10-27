using System.Linq;
using ObservableCollections;
using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        public ObservableList<TowerProxy> AllTowers { get; } = new ObservableList<TowerProxy>();

        public GameStateProxy(GameState gameState)
        {
            gameState.Towers.ForEach(building => { AllTowers.Add(new TowerProxy(building)); });

            AllTowers.ObserveAdd().Subscribe(e =>
            {
                var newBuilding = e.Value;
                gameState.Towers.Add(new Tower
                {
                    ID = newBuilding.ID,
                    Type = newBuilding.Type,
                    Level = newBuilding.Level.Value,
                    Position = newBuilding.Position.Value,
                });
            });

            AllTowers.ObserveRemove().Subscribe(e =>
            {
                var removedBuilding = e.Value;
                var building = gameState.Towers.FirstOrDefault(x => x.ID == removedBuilding.ID);
                gameState.Towers.Remove(building);
            });
        }
    }
}