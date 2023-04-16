let db;
let dbReq = indexedDB.open('myDatabase', 1);
dbReq.onupgradeneeded = function (event) {
    // Set the db variable to our database so we can use it!  
    db = event.target.result;

    let notes = db.createObjectStore('notes', { autoIncrement: true });
}
dbReq.onsuccess = function (event) {
    db = event.target.result;
    getAndDisplayNotes(db);

    addStickyNote(db, 'Sloths are awesome!');
    addStickyNote(db, 'Order more hibiscus tea');
    addStickyNote(db, 'And Green Sheen shampoo, the best for sloth fur algae grooming!');
}
dbReq.onerror = function (event) {
    alert('error opening database ' + event.target.errorCode);
}

dbReq.onupgradeneeded = function (event) {
    db = event.target.result;
}




function addStickyNote(db, message) {
    // Start a database transaction and get the notes object store
    let tx = db.transaction(['notes'], 'readwrite');
    let store = tx.objectStore('notes');
    // Put the sticky note into the object store
    let note = { text: message, timestamp: Date.now() };
    store.add(note);


// Wait for the database transaction to complete
tx.oncomplete = function () { console.log('stored note!'); }
tx.onerror = function(event) {
    alert('error storing note ' + event.target.errorCode);
};
}



//

function submitNote() {
    let message = document.getElementById('newmessage');
    addStickyNote(db, message.value);
    message.value = '';
}



function addManyNotes(db, messages) {
    let tx = db.transaction(['notes'], 'readwrite');
    let store = tx.objectStore('notes');
    for (let i = 0; i < messages.length; i++) {
        // All of the requests made from store.add are part of
        // the same transaction
        store.add({ text: messages[i], timestamp: Date.now() });
    }
    tx.oncomplete = function() {
        displayNotes(db);
        console.log('transaction complete')
    };
}


function getAndDisplayNotes(db) {
    let tx = db.transaction(['notes'], 'readonly');
    let store = tx.objectStore('notes');
    // Create a cursor request to get all items in the store, which 
    // we collect in the allNotes array
    let req = store.openCursor();
    let allNotes = [];

    req.onsuccess = function (event) {
        // The result of req.onsuccess is an IDBCursor
        let cursor = event.target.result;
        if (cursor != null) {
            // If the cursor isn't null, we got an IndexedDB item.
            // Add it to the note array and have the cursor continue!
            allNotes.push(cursor.value);
            cursor.continue();
        } else {
            displayNotes(allNotes);
        }
    }
    req.onerror = function (event) {
        alert('error in cursor request ' + event.target.errorCode);
    }
}



function displayNotes(notes) {
    console.log("notes::", notes);
    let listHTML = '<ul>';
    for (let i = 0; i < notes.length; i++) {
        let note = notes[i];
        listHTML += '<li>' + note.text + ' ' +
          new Date(note.timestamp).toString() + '</li>';
    }
    document.getElementById('notes').innerHTML = listHTML;
}
