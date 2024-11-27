document.getElementById('projectForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Ngăn chặn hành động mặc định của form

    let projectName = document.getElementById('name').value;
    let description = document.getElementById('des').value;
    let duration1 = document.getElementById('duration1').value;
    let duration2 = document.getElementById('duration2').value;
    let paymentType = document.getElementById('paymentType').value;
    let paymentAmount = document.getElementById('paymentAmount').value;
    let skills = [];
    document.querySelectorAll('input[name="skills"]:checked').forEach(skill => {
        skills.push(skill.value);
    });

    if (!projectName || !description || !duration1 || !duration2 || !paymentType || !paymentAmount || skills.length === 0) {
        alert('Please fill in all fields.');
        return;
    }

    let data = {
        job: {
            Name: projectName,
            Description: description,
            ExpectedDuration: duration1 + ' ' + duration2,
            PaymentType: paymentType,
            PaymentAmount: parseFloat(paymentAmount),
            Client: { User: { Username: 'YourUsername' } } // Adjust as necessary
        },
        SelectedSkillName: skills
    };

    fetch('/Post/Create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                window.location.href = '/Post/Success'; // Redirect to a success page
            } else {
                alert('Có lỗi xảy ra 1');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Có lỗi xảy ra 2');
        });
});
