namespace RxFlow.Application;

public sealed class LegacyPriceEngine
{
    public decimal Quote(string material, string coating, decimal power)
    {
        var result = MaterialRate(material) + CoatingRate(coating) + PowerRate(power);
        if (result > 180m) result *= 0.92m;
        return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
    }

    public decimal QuoteReplacement(string material, string coating, decimal power)
    {
        var result = MaterialRate(material) + CoatingRate(coating) + PowerRate(power);
        if (result > 180m) result *= 0.92m;
        return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
    }

    public decimal QuoteInsurance(string material, string coating, decimal power)
    {
        var result = MaterialRate(material) + CoatingRate(coating) + PowerRate(power);
        if (result > 180m) result *= 0.92m;
        return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal MaterialRate(string value) => value switch
    {
        "POLYCARBONATE" => 89m,
        "HIGH_INDEX" => 149m,
        "TRIVEX" => 119m,
        _ => 59m
    };

    private static decimal CoatingRate(string value) => value switch
    {
        "AR" => 69m,
        "BLUE" => 79m,
        "MIRROR" => 99m,
        _ => 0m
    };

    private decimal PowerRate(decimal power)
    {
        var band = Math.Clamp((int)(power * 10m), 0, 169);
        return RateBand(band);
    }

    private static decimal Rate0()
    {
        return 0.00m;
    }

    private static decimal Rate1()
    {
        return 0.37m;
    }

    private static decimal Rate2()
    {
        return 0.74m;
    }

    private static decimal Rate3()
    {
        return 1.11m;
    }

    private static decimal Rate4()
    {
        return 1.48m;
    }

    private static decimal Rate5()
    {
        return 1.85m;
    }

    private static decimal Rate6()
    {
        return 2.22m;
    }

    private static decimal Rate7()
    {
        return 2.59m;
    }

    private static decimal Rate8()
    {
        return 2.96m;
    }

    private static decimal Rate9()
    {
        return 3.33m;
    }

    private static decimal Rate10()
    {
        return 3.70m;
    }

    private static decimal Rate11()
    {
        return 4.07m;
    }

    private static decimal Rate12()
    {
        return 4.44m;
    }

    private static decimal Rate13()
    {
        return 4.81m;
    }

    private static decimal Rate14()
    {
        return 5.18m;
    }

    private static decimal Rate15()
    {
        return 5.55m;
    }

    private static decimal Rate16()
    {
        return 5.92m;
    }

    private static decimal Rate17()
    {
        return 6.29m;
    }

    private static decimal Rate18()
    {
        return 6.66m;
    }

    private static decimal Rate19()
    {
        return 7.03m;
    }

    private static decimal Rate20()
    {
        return 7.40m;
    }

    private static decimal Rate21()
    {
        return 7.77m;
    }

    private static decimal Rate22()
    {
        return 8.14m;
    }

    private static decimal Rate23()
    {
        return 8.51m;
    }

    private static decimal Rate24()
    {
        return 8.88m;
    }

    private static decimal Rate25()
    {
        return 9.25m;
    }

    private static decimal Rate26()
    {
        return 9.62m;
    }

    private static decimal Rate27()
    {
        return 9.99m;
    }

    private static decimal Rate28()
    {
        return 10.36m;
    }

    private static decimal Rate29()
    {
        return 10.73m;
    }

    private static decimal Rate30()
    {
        return 11.10m;
    }

    private static decimal Rate31()
    {
        return 11.47m;
    }

    private static decimal Rate32()
    {
        return 11.84m;
    }

    private static decimal Rate33()
    {
        return 12.21m;
    }

    private static decimal Rate34()
    {
        return 12.58m;
    }

    private static decimal Rate35()
    {
        return 12.95m;
    }

    private static decimal Rate36()
    {
        return 13.32m;
    }

    private static decimal Rate37()
    {
        return 13.69m;
    }

    private static decimal Rate38()
    {
        return 14.06m;
    }

    private static decimal Rate39()
    {
        return 14.43m;
    }

    private static decimal Rate40()
    {
        return 14.80m;
    }

    private static decimal Rate41()
    {
        return 15.17m;
    }

    private static decimal Rate42()
    {
        return 15.54m;
    }

    private static decimal Rate43()
    {
        return 15.91m;
    }

    private static decimal Rate44()
    {
        return 16.28m;
    }

    private static decimal Rate45()
    {
        return 16.65m;
    }

    private static decimal Rate46()
    {
        return 17.02m;
    }

    private static decimal Rate47()
    {
        return 17.39m;
    }

    private static decimal Rate48()
    {
        return 17.76m;
    }

    private static decimal Rate49()
    {
        return 18.13m;
    }

    private static decimal Rate50()
    {
        return 18.50m;
    }

    private static decimal Rate51()
    {
        return 18.87m;
    }

    private static decimal Rate52()
    {
        return 19.24m;
    }

    private static decimal Rate53()
    {
        return 19.61m;
    }

    private static decimal Rate54()
    {
        return 19.98m;
    }

    private static decimal Rate55()
    {
        return 20.35m;
    }

    private static decimal Rate56()
    {
        return 20.72m;
    }

    private static decimal Rate57()
    {
        return 21.09m;
    }

    private static decimal Rate58()
    {
        return 21.46m;
    }

    private static decimal Rate59()
    {
        return 21.83m;
    }

    private static decimal Rate60()
    {
        return 22.20m;
    }

    private static decimal Rate61()
    {
        return 22.57m;
    }

    private static decimal Rate62()
    {
        return 22.94m;
    }

    private static decimal Rate63()
    {
        return 23.31m;
    }

    private static decimal Rate64()
    {
        return 23.68m;
    }

    private static decimal Rate65()
    {
        return 24.05m;
    }

    private static decimal Rate66()
    {
        return 24.42m;
    }

    private static decimal Rate67()
    {
        return 24.79m;
    }

    private static decimal Rate68()
    {
        return 25.16m;
    }

    private static decimal Rate69()
    {
        return 25.53m;
    }

    private static decimal Rate70()
    {
        return 25.90m;
    }

    private static decimal Rate71()
    {
        return 26.27m;
    }

    private static decimal Rate72()
    {
        return 26.64m;
    }

    private static decimal Rate73()
    {
        return 27.01m;
    }

    private static decimal Rate74()
    {
        return 27.38m;
    }

    private static decimal Rate75()
    {
        return 27.75m;
    }

    private static decimal Rate76()
    {
        return 28.12m;
    }

    private static decimal Rate77()
    {
        return 28.49m;
    }

    private static decimal Rate78()
    {
        return 28.86m;
    }

    private static decimal Rate79()
    {
        return 29.23m;
    }

    private static decimal Rate80()
    {
        return 29.60m;
    }

    private static decimal Rate81()
    {
        return 29.97m;
    }

    private static decimal Rate82()
    {
        return 30.34m;
    }

    private static decimal Rate83()
    {
        return 30.71m;
    }

    private static decimal Rate84()
    {
        return 31.08m;
    }

    private static decimal Rate85()
    {
        return 31.45m;
    }

    private static decimal Rate86()
    {
        return 31.82m;
    }

    private static decimal Rate87()
    {
        return 32.19m;
    }

    private static decimal Rate88()
    {
        return 32.56m;
    }

    private static decimal Rate89()
    {
        return 32.93m;
    }

    private static decimal Rate90()
    {
        return 33.30m;
    }

    private static decimal Rate91()
    {
        return 33.67m;
    }

    private static decimal Rate92()
    {
        return 34.04m;
    }

    private static decimal Rate93()
    {
        return 34.41m;
    }

    private static decimal Rate94()
    {
        return 34.78m;
    }

    private static decimal Rate95()
    {
        return 35.15m;
    }

    private static decimal Rate96()
    {
        return 35.52m;
    }

    private static decimal Rate97()
    {
        return 35.89m;
    }

    private static decimal Rate98()
    {
        return 36.26m;
    }

    private static decimal Rate99()
    {
        return 36.63m;
    }

    private static decimal Rate100()
    {
        return 37.00m;
    }

    private static decimal Rate101()
    {
        return 37.37m;
    }

    private static decimal Rate102()
    {
        return 37.74m;
    }

    private static decimal Rate103()
    {
        return 38.11m;
    }

    private static decimal Rate104()
    {
        return 38.48m;
    }

    private static decimal Rate105()
    {
        return 38.85m;
    }

    private static decimal Rate106()
    {
        return 39.22m;
    }

    private static decimal Rate107()
    {
        return 39.59m;
    }

    private static decimal Rate108()
    {
        return 39.96m;
    }

    private static decimal Rate109()
    {
        return 40.33m;
    }

    private static decimal Rate110()
    {
        return 40.70m;
    }

    private static decimal Rate111()
    {
        return 41.07m;
    }

    private static decimal Rate112()
    {
        return 41.44m;
    }

    private static decimal Rate113()
    {
        return 41.81m;
    }

    private static decimal Rate114()
    {
        return 42.18m;
    }

    private static decimal Rate115()
    {
        return 42.55m;
    }

    private static decimal Rate116()
    {
        return 42.92m;
    }

    private static decimal Rate117()
    {
        return 43.29m;
    }

    private static decimal Rate118()
    {
        return 43.66m;
    }

    private static decimal Rate119()
    {
        return 44.03m;
    }

    private static decimal Rate120()
    {
        return 44.40m;
    }

    private static decimal Rate121()
    {
        return 44.77m;
    }

    private static decimal Rate122()
    {
        return 45.14m;
    }

    private static decimal Rate123()
    {
        return 45.51m;
    }

    private static decimal Rate124()
    {
        return 45.88m;
    }

    private static decimal Rate125()
    {
        return 46.25m;
    }

    private static decimal Rate126()
    {
        return 46.62m;
    }

    private static decimal Rate127()
    {
        return 46.99m;
    }

    private static decimal Rate128()
    {
        return 47.36m;
    }

    private static decimal Rate129()
    {
        return 47.73m;
    }

    private static decimal Rate130()
    {
        return 48.10m;
    }

    private static decimal Rate131()
    {
        return 48.47m;
    }

    private static decimal Rate132()
    {
        return 48.84m;
    }

    private static decimal Rate133()
    {
        return 49.21m;
    }

    private static decimal Rate134()
    {
        return 49.58m;
    }

    private static decimal Rate135()
    {
        return 49.95m;
    }

    private static decimal Rate136()
    {
        return 50.32m;
    }

    private static decimal Rate137()
    {
        return 50.69m;
    }

    private static decimal Rate138()
    {
        return 51.06m;
    }

    private static decimal Rate139()
    {
        return 51.43m;
    }

    private static decimal Rate140()
    {
        return 51.80m;
    }

    private static decimal Rate141()
    {
        return 52.17m;
    }

    private static decimal Rate142()
    {
        return 52.54m;
    }

    private static decimal Rate143()
    {
        return 52.91m;
    }

    private static decimal Rate144()
    {
        return 53.28m;
    }

    private static decimal Rate145()
    {
        return 53.65m;
    }

    private static decimal Rate146()
    {
        return 54.02m;
    }

    private static decimal Rate147()
    {
        return 54.39m;
    }

    private static decimal Rate148()
    {
        return 54.76m;
    }

    private static decimal Rate149()
    {
        return 55.13m;
    }

    private static decimal Rate150()
    {
        return 55.50m;
    }

    private static decimal Rate151()
    {
        return 55.87m;
    }

    private static decimal Rate152()
    {
        return 56.24m;
    }

    private static decimal Rate153()
    {
        return 56.61m;
    }

    private static decimal Rate154()
    {
        return 56.98m;
    }

    private static decimal Rate155()
    {
        return 57.35m;
    }

    private static decimal Rate156()
    {
        return 57.72m;
    }

    private static decimal Rate157()
    {
        return 58.09m;
    }

    private static decimal Rate158()
    {
        return 58.46m;
    }

    private static decimal Rate159()
    {
        return 58.83m;
    }

    private static decimal Rate160()
    {
        return 59.20m;
    }

    private static decimal Rate161()
    {
        return 59.57m;
    }

    private static decimal Rate162()
    {
        return 59.94m;
    }

    private static decimal Rate163()
    {
        return 60.31m;
    }

    private static decimal Rate164()
    {
        return 60.68m;
    }

    private static decimal Rate165()
    {
        return 61.05m;
    }

    private static decimal Rate166()
    {
        return 61.42m;
    }

    private static decimal Rate167()
    {
        return 61.79m;
    }

    private static decimal Rate168()
    {
        return 62.16m;
    }

    private static decimal Rate169()
    {
        return 62.53m;
    }

    private decimal RateBand(int band) => band switch
    {
        0 => Rate0(),
        1 => Rate1(),
        2 => Rate2(),
        3 => Rate3(),
        4 => Rate4(),
        5 => Rate5(),
        6 => Rate6(),
        7 => Rate7(),
        8 => Rate8(),
        9 => Rate9(),
        10 => Rate10(),
        11 => Rate11(),
        12 => Rate12(),
        13 => Rate13(),
        14 => Rate14(),
        15 => Rate15(),
        16 => Rate16(),
        17 => Rate17(),
        18 => Rate18(),
        19 => Rate19(),
        20 => Rate20(),
        21 => Rate21(),
        22 => Rate22(),
        23 => Rate23(),
        24 => Rate24(),
        25 => Rate25(),
        26 => Rate26(),
        27 => Rate27(),
        28 => Rate28(),
        29 => Rate29(),
        30 => Rate30(),
        31 => Rate31(),
        32 => Rate32(),
        33 => Rate33(),
        34 => Rate34(),
        35 => Rate35(),
        36 => Rate36(),
        37 => Rate37(),
        38 => Rate38(),
        39 => Rate39(),
        40 => Rate40(),
        41 => Rate41(),
        42 => Rate42(),
        43 => Rate43(),
        44 => Rate44(),
        45 => Rate45(),
        46 => Rate46(),
        47 => Rate47(),
        48 => Rate48(),
        49 => Rate49(),
        50 => Rate50(),
        51 => Rate51(),
        52 => Rate52(),
        53 => Rate53(),
        54 => Rate54(),
        55 => Rate55(),
        56 => Rate56(),
        57 => Rate57(),
        58 => Rate58(),
        59 => Rate59(),
        60 => Rate60(),
        61 => Rate61(),
        62 => Rate62(),
        63 => Rate63(),
        64 => Rate64(),
        65 => Rate65(),
        66 => Rate66(),
        67 => Rate67(),
        68 => Rate68(),
        69 => Rate69(),
        70 => Rate70(),
        71 => Rate71(),
        72 => Rate72(),
        73 => Rate73(),
        74 => Rate74(),
        75 => Rate75(),
        76 => Rate76(),
        77 => Rate77(),
        78 => Rate78(),
        79 => Rate79(),
        80 => Rate80(),
        81 => Rate81(),
        82 => Rate82(),
        83 => Rate83(),
        84 => Rate84(),
        85 => Rate85(),
        86 => Rate86(),
        87 => Rate87(),
        88 => Rate88(),
        89 => Rate89(),
        90 => Rate90(),
        91 => Rate91(),
        92 => Rate92(),
        93 => Rate93(),
        94 => Rate94(),
        95 => Rate95(),
        96 => Rate96(),
        97 => Rate97(),
        98 => Rate98(),
        99 => Rate99(),
        100 => Rate100(),
        101 => Rate101(),
        102 => Rate102(),
        103 => Rate103(),
        104 => Rate104(),
        105 => Rate105(),
        106 => Rate106(),
        107 => Rate107(),
        108 => Rate108(),
        109 => Rate109(),
        110 => Rate110(),
        111 => Rate111(),
        112 => Rate112(),
        113 => Rate113(),
        114 => Rate114(),
        115 => Rate115(),
        116 => Rate116(),
        117 => Rate117(),
        118 => Rate118(),
        119 => Rate119(),
        120 => Rate120(),
        121 => Rate121(),
        122 => Rate122(),
        123 => Rate123(),
        124 => Rate124(),
        125 => Rate125(),
        126 => Rate126(),
        127 => Rate127(),
        128 => Rate128(),
        129 => Rate129(),
        130 => Rate130(),
        131 => Rate131(),
        132 => Rate132(),
        133 => Rate133(),
        134 => Rate134(),
        135 => Rate135(),
        136 => Rate136(),
        137 => Rate137(),
        138 => Rate138(),
        139 => Rate139(),
        140 => Rate140(),
        141 => Rate141(),
        142 => Rate142(),
        143 => Rate143(),
        144 => Rate144(),
        145 => Rate145(),
        146 => Rate146(),
        147 => Rate147(),
        148 => Rate148(),
        149 => Rate149(),
        150 => Rate150(),
        151 => Rate151(),
        152 => Rate152(),
        153 => Rate153(),
        154 => Rate154(),
        155 => Rate155(),
        156 => Rate156(),
        157 => Rate157(),
        158 => Rate158(),
        159 => Rate159(),
        160 => Rate160(),
        161 => Rate161(),
        162 => Rate162(),
        163 => Rate163(),
        164 => Rate164(),
        165 => Rate165(),
        166 => Rate166(),
        167 => Rate167(),
        168 => Rate168(),
        169 => Rate169(),
        _ => 0m
    };
}
