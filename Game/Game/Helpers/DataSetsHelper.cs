using System.Threading.Tasks;

using Game.ViewModels;

namespace Game.Helpers
{
    public static class DataSetsHelper
    {

        private static readonly object WipeLock = new object();

        public static bool WarmUp()
        {
            ScoreIndexViewModel.Instance.GetCurrentDataSource();
            ItemIndexViewModel.Instance.GetCurrentDataSource();
            CharacterIndexViewModel.Instance.GetCurrentDataSource();
            MonsterIndexViewModel.Instance.GetCurrentDataSource();

            return true;
        }

        /// <summary>
        /// Call the Wipe routines in order one by one
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> WipeDataInSequence()
        {
            lock (WipeLock)
            {
                // -- Score
                var runSyncScore = Task.Factory.StartNew(async () =>
                {
                    await ScoreIndexViewModel.Instance.DataStoreWipeDataListAsync();
                    await Task.Delay(100);
                }).Unwrap();
                runSyncScore.Wait();

                // -- Item
                var runSyncItem = Task.Factory.StartNew(async () =>
                {
                    await ItemIndexViewModel.Instance.DataStoreWipeDataListAsync();
                    await Task.Delay(100);
                }).Unwrap();
                runSyncItem.Wait();

                // -- Character
                var runSyncCharacter = Task.Factory.StartNew(async () =>
                {
                    await CharacterIndexViewModel.Instance.DataStoreWipeDataListAsync();
                    await Task.Delay(100);
                }).Unwrap();

                // -- Monster
                var runSyncMonster = Task.Factory.StartNew(async () =>
                {
                    await MonsterIndexViewModel.Instance.DataStoreWipeDataListAsync();
                    await Task.Delay(100);
                }).Unwrap();
                runSyncCharacter.Wait();
            }

            return await Task.FromResult(true);
        }
    }
}