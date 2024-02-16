using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace hw3;

public class Calculator : INotifyPropertyChanged
{
    private string _operandA = "0";
    private string _operandB = "";
    private string _operation = "";
    private string _operation2 = "";
    private string _answer = "";

    private bool flagA = false;
    private bool flagB = false;
    private bool flagEqually = false;

    private double _digitA = 0;
    private double _digitB = 0;
    private double _digitanswer = 0;

    public void EquallyCommand()
    {
        if(flagEqually == true)
        {
            OperandA = _answer;
            Screen();
        }
        else
        {
            flagEqually = true;
            Screen();
        }
    }

    public string OperandA
    {
        get
        {
            return _operandA;
        }

        set
        {
            SetField(ref _operandA, value);
        }
    }

    public string OperandB
    {
        get
        {
            return _operandB;
        }

        set
        {
            _ = SetField(ref _operandB, value);
        }
    }

    public string Operation
    {
        get
        {
            return _operation;
        }

        set
        {
            _ = SetField(ref _operation, value);
        }
    }
    public string Operation2
    {
        get
        {
            return _operation2;
        }

        set
        {
            _ = SetField(ref _operation2, value);
        }
    }
    public string Answer
    {
        get
        {
            return _answer;
        }

        set
        {
            SetField(ref _answer, value);
        }
    }

    public void AddNumderCommand(object parameter)
    {
        if (parameter is string text)
        {
            if(_operation == "")
            {
                if(_operandA != "0")
                    OperandA = _operandA+text;
                else
                    OperandA = text;
            }
            else
            {
                if(_operation == "+" || _operation == "-" || _operation == "*" || _operation == "/" || _operation == "^" || _operation == "mod")
                {
                    if(_operandB != "0")
                        OperandB = _operandB+text;
                    else
                        OperandB = text;
                }
            }
            Screen();
        }
    }

    public void Screen()
    {
        _digitA = double.Parse(_operandA);
        if(_operandB != "")
        {
            _digitB = double.Parse(_operandB);
            switch (_operation)
            {
                case "+":
                    _digitanswer = _digitA + _digitB;
                    break;
                case "-":
                    _digitanswer = _digitA - _digitB;
                    break;
                case "*":
                    _digitanswer = _digitA * _digitB;
                    break;
                case "/":
                    if(_digitB != 0)
                        _digitanswer = _digitA / _digitB;
                    break;
                case "^":
                    _digitanswer = pow(_digitA,  _digitB);
                    break;
                case "mod":
                    if(_digitB != 0)
                        _digitanswer = _digitA % _digitB;
                    break;
                default:
                    break;
            }
            if(_digitB == 0 && (_operation == "/" || _operation == "mod" ))
                Answer = "error";
            else
                Answer = _digitanswer.ToString();
        }
        else
        {
            if(_operation2 == "")
            {
                _digitanswer = _digitA;
                Answer = _digitanswer.ToString();
            }
            else
            {
                switch (_operation2)
                {
                    case "sin":
                        _digitanswer = Math.Sin(_digitA);
                        break;
                    case "cos":
                        _digitanswer = Math.Cos(_digitA);
                        break;
                    case "tg":
                        _digitanswer = Math.Tan(_digitA);
                        break;
                    case "ceil":
                            _digitanswer = Math.Ceiling(_digitA);
                        break;
                    case "floor":
                        _digitanswer = Math.Floor(_digitA);
                        break;
                    case "!":
                        _digitanswer = Factorial(_digitA);
                        break;
                    case "ln":
                        _digitanswer = Math.Log(_digitA);
                        break;
                    case "lg":
                        _digitanswer = Math.Log10(_digitA);
                        break;
                    default:
                        break;
                }
                Answer = _digitanswer.ToString();
            }
        }
    }

    public void ClearCommand()
    {
        OperandA = "0";
        OperandB = "";
        Operation = "";
        Operation2 = "";
        flagEqually = false;
        flagA = false;
        flagB = false;
        Screen();
    }

    public void DelCommand()
    {
        if(_operandB == ""  && _operation == "" && _operation2 == "")
        {
            flagA = false;
            if(_operandA.Length != 1)
                OperandA = _operandA.Substring(0, _operandA.Length - 1);
            else
                OperandA = "0";
        }
        else {
            flagB = false;
            if(_operandB == "")
            {
                Operation = "";
                Operation2 = "";
            }
            else
            {
                OperandB = _operandB.Substring(0, _operandB.Length - 1);
            }
            if(_operandA == "0")
                Operation2 = "";
        }
        Screen();
        flagEqually = false;
    }

    public void OperationCommand(object parameter)
    {
        if (parameter is string text)
        {
            if(_operation == "" && _operation2 == "")
            {
                if((text == "+" || text == "-" || text == "*" || text == "/" || text == "^" || text == "mod"))
                {
                    Operation = text;
                    Operation2 = "";
                }
                else
                {
                    if(OperandB == "")
                    {
                        Operation2 = text;
                        Operation = "";
                    }
                }
            }else if(text == "+" || text == "-" || text == "*" || text == "/" || text == "^" || text == "mod")
            {
                Operation = text;
                Operation2 = "";
                OperandA = _answer;
                OperandB = "";
            }else
            {
                Operation2 = text;
                Operation = "";
                OperandA = _answer;
                OperandB = "";
            }
            Screen();
        }
    }
    
    public void Dot()
    {
        string text = ",";
        if(_operation == "")
        {
            if(_operandA != "" && flagA == false)
            {
                OperandA = _operandA+text;
                flagA = true;
            }
        }
        else
        {
            if(_operation == "+" || _operation == "-" || _operation == "*" || _operation == "/" || _operation == "^")
            {
                if(_operandB != "" && flagB == false)
                {
                    OperandB = _operandB+text;
                    flagB = true;
                }
            }
        }
    }

    public double pow(double baseNumber, double exponent)
    {
        double result = 1;

        for (int i = 0; i < exponent; ++i) {
            result *= baseNumber;
        }
        return result;
    }

    public static double Factorial(double a)
    {
        if (a % 1 == 0)
        {
            if (a == 0) return 1;

            if (a > 0)
            {
                int _ = 1;

                for (int i = 1; i <= a; i++)
                {
                    _ *= i;
                }
                return _;
            }
        }
        return double.NaN;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}