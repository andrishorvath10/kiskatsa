const testCases = [
    {
        name: "Egyszerű 3×3 mátrix",
        matrix: `1 1 2
                 1 1 3
                 4 5 6`,
        epsilon: 0.5,
        expectedArea: 2,
        description: "Két cella (1,1) értékkel összekapcsolva"
    },
    {
        name: "Egyforma értékek 4×4 mátrixban",
        matrix: `5 5 5 5
                 5 5 5 5
                 5 5 5 5
                 5 5 5 5`,
        epsilon: 1.0,
        expectedArea: 16,
        description: "Minden cella egyforma értékkel, az összes egy területet képez"
    },
    {
        name: "Emelkedő terület 4×4 mátrixban",
        matrix: `1 2 3 4
                2 3 4 5
                3 4 5 6
                4 5 6 7`,
        epsilon: 1.0,
        expectedArea: 16,
        description: "Értékek 1-től 7-ig, minden szomszédos különbség <= 1"
    },
    {
        name: "Szigetek 5×5 mátrixban",
        matrix: `1 1 9 9 9
                1 1 9 3 3
                9 9 9 3 3
                4 4 9 9 9
                4 4 9 5 5`,
        epsilon: 0.5,
        expectedArea: 9,
        description: "Több sziget, a legnagyobb területe 9 cella"
    },
    {
        name: "Nagyobb epszilon érték tesztelése",
        matrix: `1 3 5 7
                2 4 6 8
                9 7 5 3
                8 6 4 2`,
        epsilon: 2.5,
        expectedArea: 16,
        description: "Nagyobb epszilon érték (2.5) az összes cellát összeköti"
    },
    {
        name: "Éles határú terület",
        matrix: `1 1 1 1 1
                1 9 9 9 1
                1 9 9 9 1
                1 9 9 9 1
                1 1 1 1 1`,
        epsilon: 0.5,
        expectedArea: 16,
        description: "A külső és belső terület élesen elválik, a külső terület nagyobb"
    }
];