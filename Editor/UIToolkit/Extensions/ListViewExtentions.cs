using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    public static class ListViewExtensions
    {
        /// <summary>
        /// Rebuild/refresh ListView in compatible mode with Unity 2019/2021 editor
        /// </summary>
        /// <param name="listView"></param>
        public static void RebuildInCompatibleMode(this ListView listView)
        {
#if UNITY_2019_4_40
            listView.Refresh();      
#else
            listView.Rebuild();
#endif
        }
    }
}