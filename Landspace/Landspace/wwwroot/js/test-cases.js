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
        expectedArea: 4,
        description: "Több sziget, a legnagyobb területe 4 cella"
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
        name: "Negatív értékek tesztelése",
        matrix: `-1 -1 -1 5
-1 -2 -1 5
-1 -1 -1 5
5 5 5 5`,
        epsilon: 1.0,
        expectedArea: 8,
        description: "Negatív értékek a mátrixban, két különálló terület"
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

function loadTestCase(index) {
    const testCase = testCases[index];
    if (!testCase) return;
    
    document.getElementById('matrixInput').value = testCase.matrix;
    document.getElementById('epsilonInput').value = testCase.epsilon;
    console.error("adfasdf");
    const resultArea = document.getElementById('resultArea');
    resultArea.innerHTML = `
        <div class="alert alert-info">
            <h5>${testCase.name}</h5>
            <p><strong>Leírás:</strong> ${testCase.description}</p>
            <p><strong>Várt legnagyobb terület:</strong> ${testCase.expectedArea} cella</p>
        </div>
    `;
}

function initializeTestCases() {
    const testSelector = document.createElement('div');
    testSelector.className = 'mb-3';
    testSelector.innerHTML = `
        <label for="testCaseSelect" class="form-label">Teszt esetek:</label>
        <select class="form-select" id="testCaseSelect">
            <option value="">Válassz teszt esetet...</option>
            ${testCases.map((test, index) => `<option value="${index}">${test.name}</option>`).join('')}
        </select>
    `;
    
    const matrixInputContainer = document.querySelector('label[for="matrixInput"]').parentNode;
    matrixInputContainer.parentNode.insertBefore(testSelector, matrixInputContainer);
    console.error("jooo");
    document.getElementById('testCaseSelect').addEventListener('change', function() {
        const selectedIndex = this.value;
        if (selectedIndex !== '') {
            loadTestCase(parseInt(selectedIndex));
        }
    });
} 