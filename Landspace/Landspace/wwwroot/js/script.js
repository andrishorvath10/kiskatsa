document.addEventListener('DOMContentLoaded', function() {
    const calculateBtn = document.getElementById('calculateBtn');
    const matrixInput = document.getElementById('matrixInput');
    const epsilonInput = document.getElementById('epsilonInput');
    const resultArea = document.getElementById('resultArea');
    const matrixVisualization = document.getElementById('matrixVisualization');

    if (typeof initializeTestCases === 'function') {
        initializeTestCases();
    }

    const apiUrl = '/api/landarea/calculate';

    calculateBtn.addEventListener('click', async function() {
        const matrixText = matrixInput.value.trim();
        const epsilon = parseFloat(epsilonInput.value);
        
        if (!matrixText) {
            showError('A mátrix nem lehet üres.');
            return;
        }

        if (isNaN(epsilon)) {
            showError('Az epszilon értéknek számnak kell lennie.');
            return;
        }

        try {
            const matrix = parseMatrix(matrixText);
            
            if (!isSquareMatrix(matrix)) {
                showError('A mátrixnak négyzetes formájúnak (n×n) kell lennie.');
                return;
            }

            sendToBackend(matrix, epsilon);
        } catch (error) {
            showError('Hiba a mátrix feldolgozása során: ' + error.message);
        }
    });

    function parseMatrix(text) {
        const rows = text.split('\n').filter(row => row.trim() !== '');
        const matrix = rows.map(row => {
            const values = row.split(/\s+/).map(val => parseInt(val.trim()));
            if (values.some(isNaN)) {
                throw new Error('A mátrix csak egész számokat tartalmazhat.');
            }
            return values;
        });
        return matrix;
    }

    function isSquareMatrix(matrix) {
        const n = matrix.length;
        return matrix.every(row => row.length === n);
    }

    async function sendToBackend(matrix, epsilon) {
        try {
            
            
            const response = await fetch(apiUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    matrix: matrix,
                    epsilon: epsilon
                })
            });

            if (!response.ok) {
                throw new Error(`HTTP hiba: ${response.status}`);
            }

            const result = await response.json();
            displayResult(result);
        } catch (error) {
            showError('Hiba a szerver kommunikáció során: ' + error.message);
        }
    }

    function displayResult(result) {
        resultArea.innerHTML = `
            <p><strong>Epszilon érték:</strong> ${result.epsilon}</p>
            <p><strong>Legnagyobb összefüggő terület mérete:</strong> ${result.largestAreaSize} cella</p>
        `;

        createMatrixVisualization(result.matrix, result.largestAreaIndices);
    }

    function createMatrixVisualization(matrix, largestAreaIndices) {
        const tableHtml = document.createElement('table');
        tableHtml.className = 'matrix-table';
        
        const areaMap = {};
        largestAreaIndices.forEach(index => {
            areaMap[`${index.row},${index.col}`] = true;
        });

        for (let i = 0; i < matrix.length; i++) {
            const row = document.createElement('tr');
            
            for (let j = 0; j < matrix[i].length; j++) {
                const cell = document.createElement('td');
                cell.textContent = matrix[i][j];
                
                if (areaMap[`${i},${j}`]) {
                    cell.classList.add('largest-area');
                }
                
                row.appendChild(cell);
            }
            
            tableHtml.appendChild(row);
        }

        matrixVisualization.innerHTML = '';
        matrixVisualization.appendChild(tableHtml);
    }

    function showError(message) {
        resultArea.innerHTML = `<div class="alert alert-danger">${message}</div>`;
        matrixVisualization.innerHTML = '';
    }
}); 