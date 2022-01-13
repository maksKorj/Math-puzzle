
public abstract class MathSign : GridContent
{
    public abstract int EqualTo(int firstNumber, int secondNumber);

    public abstract int EqualTo(out int firstNumber, out int secondNumber, int minSize);
}
