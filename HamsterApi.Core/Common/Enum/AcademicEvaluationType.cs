namespace HamsterApi.Core.Common.Enum;

/// <summary>
/// Представляет тип оценивания академической нагрузки.
/// </summary>
[Flags]
public enum AcademicEvaluationType
{
    /// <summary>
    /// Оценка за зачет.
    /// </summary>
    Credit,
    /// <summary>
    /// Экзаменационная оценка.
    /// </summary>
    Exam,
    /// <summary>
    /// Зачет с оценкой.
    /// </summary>
    CreditWithAnAssessment,
    /// <summary>
    /// РГР + что-то.
    /// </summary>
    Rgr
}
