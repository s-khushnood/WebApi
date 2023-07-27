$(document).ready(() => {
  var students;
  const POSTS_API = "http://localhost:3013/GetListOfStudents";

  // fetch and display posts for the first time

  fetchstudents();

  // updateStats();

  // function to render students available in the array students[]

  function displaystudents() {
    $(".posts-container").empty();

    for (let i = 0; i < students.length; i++) {
      $(".posts-container").append(makePost(students[i]));
    }
  }

  // function to make a post

  function makePost(student) {
    return `

                <div class="post">

                    <h2>${student.studentId}</h2>

                    <p>${student.firstName + student.lastName}</p>

                    <p>${student.studentAddress}</p>

                    <p>${student.phoneNumber}</p>

                    <p>${student.studentAge}</p>

 

                </div>

                `;
  }

  function displayError(error) {
    $(".posts-container").html(
      '<div class="error">Some Error has occured!</div>'
    );
  }

  function fetchstudents() {
    $.ajax({
      method: "GET",

      url: POSTS_API,

      success: (data) => {
        students = data;

        displaystudents();
      },

      error: displayError,
    });
  }

    // Function to handle form submission
    $("#studentForm").submit(function (event) {
      event.preventDefault(); // Prevent the default form submission

      // Gather form data
      const formData = {
          firstName: $("#firstName").val(),
          lastName: $("#lastName").val(),
          studentAddress: $("#studentAddress").val(),
          studentAge: $("#studentAge").val(),
          phoneNumber: $("#phoneNumber").val(),
      };

      // Make an AJAX POST request
      $.ajax({
          type: "POST",
          url: "http://localhost:3013/InsertStudent", // Replace this with your actual API endpoint URL
          data: JSON.stringify(formData),
          contentType: "application/json",
          success: function (_data) {
              // Handle successful response from the server
              alert("Student added successfully!");
              // You can perform additional actions here, like clearing the form or showing a success message.
          },
          error: function (error) {
              // Handle error response from the server
              alert("Failed to add student because of: " + error + ". Please try again!");
              // You can perform additional error handling here.
          },
      });
  });
});
