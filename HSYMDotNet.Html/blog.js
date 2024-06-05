const tblBlog = "blogs";

//createBlog();
// updateBlog("28ea95f0-f8ba-416d-9606-a5587e4dbbe8",'ddddd','dddd','ddddd');
deleteBlog("aac21a62-e1d1-43d5-9347-a2244e6711bb");

//Read
function readBlog() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
}

//Create
function createBlog() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    const requestModel = {
        id: uuidv4(),
        title: "For food",
        author: "author1",
        content: "food content",
    };

    lst.push(requestModel);

    const jsonBLog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBLog);
    //localStorage.setItem("blogs" ,requestModel);

}

//Update
function updateBlog(id, title, author, content) {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs)
    }
    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length);

    if (items.length == 0) {
        console.log("no data found >_<");
        return;
    }
    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;
}


//Delete
function deleteBlog(id) {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs)
    }
    const items = lst.filter(x => x.id === id);
    console.log(items);

    if (items.length == 0) {
        console.log("no data found >_<");
        return;
    }

    lst = lst.filter(x => x.id !== id);
    const jsonBLog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBLog);
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}