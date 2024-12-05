
console.log("js good");
async function editTodo(id) {
    console.log(`id: ${id}`);
    document.getElementById(`editRow-${id}`).style.display = "table-row";
}
async function saveTodo(id) {
    let title = document.getElementById(`Title-${id}`).value;
    let description = document.getElementById(`Description-${id}`).value;
    let dueTime = document.getElementById(`DueTime-${id}`).value;
    let isComplete = document.getElementById(`isComplete-${id}`).checked;
    let percentDone = parseFloat(document.getElementById(`percent_done-${id}`).value);
    console.log(percentDone);
    const todo = {
        Title: title,
        Description: description,
        DueTime: dueTime,
        isComplete: isComplete,
        percent_done: percentDone,
    };
    try {
        const response = await fetch(`/todos/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(todo),
        });
        if (response.ok) {
            alert("Todo updated");
            window.location.reload();
        }
        else {
            alert("update failed");
        }
    } catch {
        console.error("Error", error);
    }
}
function cancelEdit(id) {
   document.getElementById(`editRow-${id}`).style.display = "none";
}
async function deleteTodo(id) {
    try {
        const response = await fetch(`/todos/${id}`, {
            method: "DELETE",   
        });
        if (response.ok) {
            alert("Todo deleted");
            window.location.reload();
        }
        else {
            const error = await response.text();
            alert("Deletion failed");
        }
    } catch {
        console.error("error", error);
        alert("ERROR");
    }
}