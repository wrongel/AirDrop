using System.Collections.Generic;

// Класс самолета
class Aircraft
{
    public int m_nType;     // Вид. 1 - Ан-12; 2 - Ан-124; 3 - Ил-76
    public string m_sType;  // Название
    public int m_nAmount;   // Количество однотипно загруженных самолетов

    double m_dMass;         // Полезная нагрузка
    double m_dArea;         // Площадь грузовой кабины
    double m_dHeight;       // Высота грузовой кабины

    double m_dFreeMass;     // Сколько веса еще можно взять на борт
    double m_dFreeArea;     // Свободное место по площади

    List<int> m_Cargos;     // Список типов грузов
    int m_nCargoLimit;      // Ограничение по количеству грузов

    int m_nSoloPpl;         // Сколько парашютистов можно взять без груза
    int m_nCargoPpl;        // Сколько парашютистов можно взять с грузом
    int m_nFreePpl;         // Сколько парашютистов еще можно взять
    int m_nPpl;             // Сколько парашютистов фактически на борту

    double m_dTser;             // Время серии
    const int    c_nTir = 7;    // Время между грузами, используется при подсчете времени серии
    const double c_dTip = 0.7;  // Время между людьми, используется при подсчете времени серии
    const int    c_nM = 2;      // Количество потоков, используется по подчете времени серии

    // Конструктор
    public Aircraft(int nType, double dMass, double dLength, double dWidth, double dHeight)
    {
        // Инициализация полей
        m_Cargos = new List<int>();
        m_nAmount = 1;
        m_nType = nType;

        m_dMass = m_dFreeMass = dMass;
        m_dArea = m_dFreeArea = dLength * dWidth;
        m_dHeight =  dHeight;

        m_nPpl = 0;
        // В зависимости от типа разные параметры
        switch (nType)
        {
            case 1:
                m_sType = "Ан - 12";
                m_nSoloPpl = m_nFreePpl = 60;
                m_nCargoPpl = 0;
                m_nCargoLimit = 2;
                break;
            case 2:
                m_sType = "Ан - 124";
                m_nSoloPpl = m_nFreePpl = 440;
                m_nCargoPpl = 50;
                m_nCargoLimit = 5;
                break;
            case 3:
                m_sType = "Ил - 76";
                m_nSoloPpl = m_nFreePpl = 126;
                m_nCargoPpl = 21;
                m_nCargoLimit = 3;
                break;
        }
    }

    // Добавить груз на борт
    public int AddCargo(int nType, double dMass, double dLength, double dWidth, double dHeight)
    {
        // Если есть превышение по габаритам
        if ((dMass > m_dFreeMass) || ((dLength * dWidth) > m_dFreeArea) || (dHeight > m_dHeight))
            return 0;

        // Если есть превышение по количеству
        if (m_Cargos.Count == m_nCargoLimit)
            return 0;

        // Груз загружен
        m_dFreeMass -= dMass;               // Уменьшить доступную для загрузки массу
        m_dFreeArea -= (dLength * dWidth);  // Уменьшить доступную для загрузки площадб
        m_Cargos.Add(nType);                // Добавить тип груза в список
        m_nFreePpl = m_nCargoPpl;           // Когда добавляем груз, места под людей меньше

        return 1;
    }

    // Добавить парашютистов. Возвращаемое значение - количество не загруженных парашютистов
    public int AddPpl(int nPplCount)
    {
        int nPar;       // Сколько парашютистов будет добавлено
        int nRet;       // Сколько парашютистов не будет добавлено

        // Если места под людей больше, чем надо добавить
        if (m_nFreePpl >= nPplCount)
        {
            nPar = nPplCount;           // Добавляем всех
            m_nFreePpl -= nPplCount;    // Уменьшение количества свободных мест
            nRet = 0;                   // Все парашютисты добавлены
        }
        // Если места под людей меньше, чем надо добавить, то добавляем сколько можем
        else
        {
            nPar = m_nFreePpl;          // Парашютисты заняли все свободные места
            m_nFreePpl = 0;             // Свободных мест нет
            nRet = nPplCount - nPar;    // Сколько не добавили
        }
        m_nPpl += nPar;     // Прибавляем к общему количеству на борту

        return nRet;
    }

    // Посчитать время серии
    public double CalcSerTime()
    {
        // Для самолетов с грузами и парашютистами
        if (m_Cargos.Count > 0)
            m_dTser = ((m_Cargos.Count - 1) * c_nTir) + 4 + ((m_nPpl * c_dTip) / c_nM);     // Формула (5)
        // Для самолетов только с парашютистами
        else
            m_dTser = (m_nPpl * c_dTip) / c_nM;     // Формула (6)

        return m_dTser;
    }

    // Вернуть список id грузов
    public string GetListOfCargos()
    {
        string strCargos = "";

        foreach(int Cargo in m_Cargos)
            strCargos += string.Format("{0} ", Cargo);

        return strCargos;
    }

    // Получить количество грузов на борту
    public int GetNumOfCargos()
    {
        return m_Cargos.Count;
    }

    // Получить количество парашютистов на борту
    public int GetNumOfPpl()
    {
        return m_nPpl;
    }
}
