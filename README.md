<h1>NottCS</h1>
<p>This is a student run club project by the Computer Science Society (CSS) of The University of Nottingham Malaysia Campus</p>

<h2>Background</h2>
<p>There are many processes that clubs and societies in UNMC have to go through that are currently tedious which could be simplified by a lot.</p>
<ul>
  <li>
    <h3>Troublesome event sign up</h3>
    <p>Firstly, for an event participant, it is and troublesome to sign up for events by looking through email and finding the corresponding event promotional email, and then finding the appropriate event sign up link to sign up. On the other hand, for an event organizer, it is also unreliable to use email to promote an event as the email could easily be missed among many others, and it is tedious to check whether each participant has club membership or not. Worse, when an event requires payment, the event organizer has to check whether the participant has club membership, and then determine the appropriate pricing, and then finally manually update a spreadsheet that contains a list of participants for that event after collecting payment.</p>
  </li>
  <li>
    <h3>Hard to update event participants</h3>
    <p>Furthermore, it is also troublesome to individually add each participant as a recipient when an event organizer wants to send out updates accordingly through email if there are any changes to the event.</p>
  </li>
  <li>
    <h3>Troublesome attendance taking</h3>
    <p>
      Moreover, during an event, when event organisers require recording attendance for s-CPD points or any other reason, it requires them to go through the hassle of borrowing a barcode scanner from the department.
    </p>
  </li>
  <li>
    <h3>Impossible to collect anonymous feedback</h3>
    <p>In addition, after an event, it is virtually impossible to collect feedback from participants anonymously without getting rubbish responses from non-participants. Anonymity is important when collecting feedback as it guarantees that the event organisers will never be able to discriminate anyone in future events based on their feedback of previous events.</p>
  </li>
  <li>
  <h3>Manual event reminder</h3>
  <p>Finally, some participants who are not good at remembering things may forget that they signed up for a particular event, and as such are in need of some automated way to add a calendar entry to remind themselves before the event happens.</p>
  </li>
</ul>

<h2>Problem Solving</h2>
<p>In order to simplify clubs and society processes in the context of UNMC such as event registration, meeting arrangement, event attendance taking for s-CPD, etc., a mobile app that is usable on both iOS and android is to be developed. It has to feature at least the following functions:</P>
<ol>
  <li>One-click event registration after log in</li>
  <li>Privilege control within each club (using OWA or otherwise)</li>
  <li>Ability for admins of a club to finalise event registration of any individual through unique QR code or bar code from student ID (for events with payments, optional)</li>
  <li>Ability for admins to confirm attendance of any event through unique QR code or bar code</li>
  <li>Push notification for event updates (only for signed up events)</li>
  <li>Secure account database (password hashing with salt)</li>
  <li>Account sign up using OWA (manual entry, if wrong then nobody can add you to club…)</li>
  <li>Event feedback and satisfaction rating (with an option to remain anonymous)</li>
</ol>
