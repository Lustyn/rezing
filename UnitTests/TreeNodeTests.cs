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
    }
}
