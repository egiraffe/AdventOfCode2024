namespace Solutions.Solutions._01;

public class A01 : ISolution
{
    public object Execute()
    {
        return GetCalibrationValue(Inputs.A);
    }

    protected virtual int GetCalibrationValue(string input)
    {
        return input
            .Split(Environment.NewLine)
            .Sum(s => GetCalibrationValueOfLine(s.ToArray()));
    }

    protected virtual int GetCalibrationValueOfLine(char[] line)
    {
        var digits = GetLineDigits(line);
        return digits.Length > 0 ? digits[0] * 10 + digits[^1] : 0;
    }

    protected virtual int[] GetLineDigits(char[] line)
    {
        return line.Where(char.IsDigit).Select(s => s - '0').ToArray();
    }
}