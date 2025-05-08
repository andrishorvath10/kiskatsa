document.addEventListener("DOMContentLoaded", function () {
    const matrixinput = document.getElementById("matrixinput");
    const epsziloninput = document.getElementById("epsziloninput");
    const calculatebutton = document.getElementById("calculatebutton");
    const resultarea = document.getElementById("resultarea");
    const matrixvisualization = document.getElementById("matrixvisualization");
    const apiurl = "api/landarea/calculate";

    calculatebutton.addEventListener("click", async function () {
        const matrixText = matrixinput.value.trim();
        const epszilon = parseFloat(epsziloninput.value);

        if (!matrixText) {
            showError("Matrix input cannot be empty.");
            return;
        }
        if (isNaN(epszilon)) {
            showError("Epsilon must be a number.");
            return;
        }

        try {
            const matrix = parseMatrixInput(matrixText);
            if (!isValidMatrix(matrix)) {
                showError("Invalid matrix format.");
                return;
            }
            SendtoBackend(matrix, epszilon);
        }
        catch (error) {
            showError("An error occurred while processing the request: " + error.message);
        }
    });
    function parseMatrixInput(input) {
        const rows = input.split("\n").filter(row => row.trim() !== "");
        const matrix = rows.map(row => {
            const values = row.split("/\s+/").map(value => parseInt(value.trim()));
            if (values.some(isNaN)) {
                throw new Error("Invalid matrix format.");
            }
            return values;
        });
        return matrix;
    }

    function isValidMatrix(matrix) {
        
    }

    async function SendtoBackend(matrix, epszilon) {
        
    }

    function displayResult(result) {
        
    }

    function displayMatrix(matrix, largestareaindexes) {

    }



});