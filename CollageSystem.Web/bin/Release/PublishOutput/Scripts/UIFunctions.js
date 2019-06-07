function OnSuccessSemStu() {
    var url = "/GP/Administration/SemesterCourses/OnSuccess";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnFailureSemStu() {
    var url = "/GP/Administration/SemesterCourses/OnFailure";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function EditMaxHours(StuID) {
    var url = "/GP/Administration/SemesterCourses/EditMaxHours?StuID=" + StuID;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
$("#ul-bar li").click(function () {
    $(this).toggleClass('active');
});
function OnFailureMarks() {
    var url = "/GP/Control/SemesterMarks/OnFailure";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnSuccessMarks() {
    var url = "/GP/Control/SemesterMarks/OnSuccess";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function DepCourses(DepID) {
    if (DepID == undefined || DepID == null || DepID == "" || DepID == 0) {
        alert("يجب اختيار القسم");
    } else {
        document.location.href = '/GP/Administration/SemesterCourses/CoursesForDepartment?DepID=' + DepID;

    }

}

function EditStuMarks(SemStuID , CourseID) {
    //alert('hi' + id )
    var url = "/GP/Control/SemesterMarks/Edit?SemStuID=" + SemStuID + "&CourseID=" + CourseID;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function GoHome() {
    //alert('hi' + id )
    var url = "/GP/Administration/Semesters/Index";
    document.location.href = url;
}
function CreateCourse() {
    //alert('hi' + id )
    var url = "/GP/Administration/Courses/Create";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function EditCourse(id) {
    //alert('hi' + id )
    var url = "/GP/Administration/Courses/Edit?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function DeleteCourse(id) {
    //alert('hi' + id )
    var url = "/GP/Administration/Courses/Delete?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}

function CoursesRef() {
    var url = "/GP/Administration/CourseRef/Index";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function CreateCourseRef() {
    var url = "/GP/Administration/CourseRef/Create";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function DeleteCourseRef(id) {
    var url = "/GP/Administration/CourseRef/Delete?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function EditCourseRef(id) {
    var url = "/GP/Administration/CourseRef/Edit?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
//sss
function ShowStuCourses(StuID) {
    var url = "/GP/Administration/SemesterCourses/ShowSemesterCoursesForStu?StuID=" + StuID;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}

function CreateDepartments() {
    var url = "/GP/Administration/Departments/Create";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function DeleteDepartments(id) {
    var url = "/GP/Administration/Departments/Delete?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function EditDepartments(id) {
    var url = "/GP/Administration/Departments/Edit?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}

function CreateSem() {
    var url = "/GP/Administration/Semesters/Create";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
EditSem


function EditSem(id) {
    var url = "/GP/Administration/Semesters/Edit?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}

function AddCourseToStudient() {
    var url = "/GP/Student/GP/StudentSemesters/AddCourses";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function CreateIns() {
    var url = "/GP/Administration/Instroctor/Create";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function DeleteIns(id) {
    var url = "/GP/Administration/Instroctor/Delete?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function EditIns(id) {
    var url = "/GP/Administration/Instroctor/Edit?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}


function CreateStu() {
    var url = "/GP/Administration/GP/Student/Create";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function DeleteStu(id) {
    var url = "/GP/Administration/GP/Student/Delete?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}
function EditStu(id) {
    var url = "/GP/Administration/GP/Student/Edit?id=" + id;

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })

}

function OnFailure() {
    var url = "/GP/Administration/Courses/OnFailure";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnSuccess() {
    var url = "/GP/Administration/Courses/OnSuccess";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}


function OnFailureStu() {
    var url = "/GP/Administration/GP/Student/OnFailure";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnSuccessStu() {
    var url = "/GP/Administration/GP/Student/OnSuccess";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnFailureIns() {
    var url = "/GP/Administration/Instroctor/OnFailure";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnSuccessIns() {
    var url = "/GP/Administration/Instroctor/OnSuccess";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnFailureDep() {
    var url = "/GP/Administration/Departments/OnFailure";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}
function OnSuccessDep() {
    var url = "/GP/Administration/Departments/OnSuccess";

    $("#myModalBodyDiv1").load(url, function () {
        $("#myModal1").modal("show");

    })
}

$(function () {
    $("#accordion").accordion({
        collapsible: true,
        active: false,
    });

});
