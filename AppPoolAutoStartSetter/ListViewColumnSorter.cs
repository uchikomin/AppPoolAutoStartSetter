using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppPoolAutoStartSetter
{
    // 参考
    // https://learn.microsoft.com/ja-jp/troubleshoot/developer/visualstudio/csharp/language-compilers/sort-listview-by-column

    /// <summary>
    /// ListViewColumnソートクラス
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn { set; get; } = 0;

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order { set; get; } = SortOrder.None;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer comparer;

        /// <summary>
        /// Class constructor. Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the CaseInsensitiveComparer object
            comparer = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            // いずれかが null の場合、0 を返す（基本的にはありえないはず）
            if (x == null || y == null)
            {
                return 0;
            }

            // Cast the objects to be compared to ListViewItem objects
            // リストビュー項目
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;

            // 比較
            int compareResult = comparer.Compare(itemX.SubItems[SortColumn].Text, itemY.SubItems[SortColumn].Text);

            // Asc の場合
            if (Order == SortOrder.Ascending)
            {
                // 比較結果を返す
                return compareResult;
            }
            // Desc の場合
            else if (Order == SortOrder.Descending)
            {
                // 比較結果を反転して返す
                return (-compareResult);
            }
            // それ以外の場合
            else
            {
                // 0 を返す
                return 0;
            }
        }
    }
}
