namespace UnitTests;

[TestClass]
public class TreeNodeTests
{
    [TestMethod]
    public void BasicCase()
    {
        var root = TreeNode.Read(@"
        Root
        {
            Test
            {
                ItWorks = ""Maybe?""
                Yes = 1
                ItDoes = 1.0
                Hopefully = 1.0 2.0 3.0
                CanIt = 1 2 3
            }
        }
        ");

        Assert.AreEqual("Root", root.Name);
        Assert.AreEqual(1, root.Children.Count);
        Assert.AreEqual("Test", root.Children[0].Name);
        Assert.IsInstanceOfType<TreeNode.Object>(root.Children[0]);
        var test = (TreeNode.Object)root.Children[0];
        Assert.AreEqual("ItWorks", test.Children[0].Name);
        Assert.IsInstanceOfType<TreeNode.String>(test.Children[0]);
        var itWorks = (TreeNode.String)test.Children[0];
        Assert.AreEqual("Maybe?", itWorks.Value);
        Assert.AreEqual("Yes", test.Children[1].Name);
        Assert.IsInstanceOfType<TreeNode.Int>(test.Children[1]);
        var yes = (TreeNode.Int)test.Children[1];
        Assert.AreEqual(1, yes.Value);
        Assert.AreEqual("ItDoes", test.Children[2].Name);
        Assert.IsInstanceOfType<TreeNode.Float>(test.Children[2]);
        var itDoes = (TreeNode.Float)test.Children[2];
        Assert.AreEqual(1.0f, itDoes.Value);
        Assert.AreEqual("Hopefully", test.Children[3].Name);
        Assert.IsInstanceOfType<TreeNode.FloatArray>(test.Children[3]);
        var hopefully = (TreeNode.FloatArray)test.Children[3];
        Assert.AreEqual(3, hopefully.Values.Count);
        Assert.AreEqual(1.0f, hopefully.Values[0]);
        Assert.AreEqual(2.0f, hopefully.Values[1]);
        Assert.AreEqual(3.0f, hopefully.Values[2]);
        Assert.AreEqual("CanIt", test.Children[4].Name);
        Assert.IsInstanceOfType<TreeNode.IntArray>(test.Children[4]);
        var canIt = (TreeNode.IntArray)test.Children[4];
        Assert.AreEqual(3, canIt.Values.Count);
        Assert.AreEqual(1, canIt.Values[0]);
        Assert.AreEqual(2, canIt.Values[1]);
        Assert.AreEqual(3, canIt.Values[2]);
    }
}
